using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        TileMap tileMap;

        Map map;

        int columnCount, rowCount;

        int currentRow, currentColumn;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadTileMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "c:\\";
            fileDialog.Filter = "Images (*.png *.jpg)|*.png;*.jpg";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                tileMap = new TileMap(fileDialog.FileName);
                DisplayTileMapProperties();
            }
        }

        private void DisplayTilemap()
        {
            Graphics graphics = TileCanvas.CreateGraphics();
            graphics.Clear(Color.White);
            Image img = tileMap.imageSource;
            int cpt = 0;
            for (int x = 0; x < columnCount; x++)
            {
                for(int y = 0; y < rowCount; y++)
                {
                    if (cpt < tileMap.tileCount)
                    {
                        graphics.DrawImage(img,
                            x * (tileMap.tileWidth + 1),
                            y * (tileMap.tileHeight + 1),
                            tileMap.GetTile(cpt).GetImagePosition(),
                            GraphicsUnit.Pixel
                        );
                        cpt++;
                    }
                }
            }
        }

        private void DisplayMap()
        {
            Graphics g = MapCanvas.CreateGraphics();
            g.Clear(Color.White);
            for(int x = 0; x <map.width; x++) {
                for(int y = 0; y < map.height; y++)
                {
                    if (map.GetTile(x, y)>-1)
                    {
                        g.DrawImage(tileMap.imageSource,
                            x * tileMap.tileWidth,
                            y * tileMap.tileHeight,
                            tileMap.GetTile(map.GetTile(x, y)).GetImagePosition(),
                            GraphicsUnit.Pixel
                        );
                    }
                }
            }
        }

        private void DisplayTileMapProperties()
        {
            PanelProperties.Visible = true;
            PanelProperties.BringToFront();
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            DisplayTilemap();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            XMLHandler.SaveMapAsTML(tileMap);
        }

        private void LoadMap_Click(object sender, EventArgs e)
        {
            //XmlDocument doc = XMLHandler.LoadMapFromTML();
        }

        private void ValidateTileMapProp_Click(object sender, EventArgs e)
        {
            PanelProperties.Visible = false;
            PanelProperties.Left = 797;
            PanelProperties.SendToBack();
            tileMap.Cut((int)TileWidth.Value, (int)TileHeight.Value, (int)MarginWidth.Value,(int)MarginHeight.Value);
            columnCount = tileMap.tileCountX;
            rowCount = tileMap.tileCountY;
            TileCanvas.Width = columnCount * (tileMap.tileWidth + 1) + 1;
            TileCanvas.Height = rowCount * (tileMap.tileHeight + 1) + 1;
            map = new Map(MapCanvas.Width/tileMap.tileWidth,MapCanvas.Height/tileMap.tileHeight);
            DisplayTilemap();
            DisplayMap();
        }

        private void TileCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (tileMap != null)
            {
                int previousColumn = currentColumn;
                int previousRow = currentRow;
                currentColumn = (e.X / (tileMap.tileWidth + 1));
                currentRow = (e.Y / (tileMap.tileHeight + 1));
                if (previousColumn!=currentColumn||previousRow!=currentRow)
                {
                    int x = currentColumn * (tileMap.tileWidth + 1);
                    int y = currentRow * (tileMap.tileHeight + 1);
                    DisplayTilemap();
                    Graphics graphics = TileCanvas.CreateGraphics();
                    graphics.DrawRectangle(new Pen(Color.Gray, 2), x, y, tileMap.tileWidth, tileMap.tileHeight);
                }
            }
        }
    }

    static class XMLHandler
    {
        public static void SaveMapAsTML(TileMap tileMap = null, Map map=null)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Map file (*.tml)|*.tml";
            saveFile.Title = "Save the map";
            saveFile.ShowDialog();

            if (saveFile.FileName != "")
            {
                XmlTextWriter textWriter = new XmlTextWriter(saveFile.FileName,Encoding.UTF8);
                textWriter.WriteStartDocument();

                textWriter.WriteStartElement(TileMapElement.Name);

                    textWriter.WriteStartElement(TileMapElement.Source);
                    textWriter.WriteString(tileMap.source);
                    textWriter.WriteEndElement();

                    textWriter.WriteStartElement(TileMapElement.MarginWidth);
                    textWriter.WriteValue(tileMap.marginWidth);
                    textWriter.WriteEndElement();

                    textWriter.WriteStartElement(TileMapElement.MarginHeight);
                    textWriter.WriteValue(tileMap.marginHeight);
                    textWriter.WriteEndElement();

                    textWriter.WriteStartElement(TileMapElement.TileWidth);
                    textWriter.WriteValue(tileMap.tileWidth);
                    textWriter.WriteEndElement();

                    textWriter.WriteStartElement(TileMapElement.TileHeight);
                    textWriter.WriteValue(tileMap.tileHeight);
                    textWriter.WriteEndElement();

                textWriter.WriteEndElement();

                textWriter.WriteEndDocument();
                textWriter.Close();
            }
        }

        public static XmlDocument LoadMapFromTML()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "c:\\";
            fileDialog.Filter = "Map files (*.tml)|*.tml";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileDialog.FileName);
                return xmlDoc;
            }
            return null;
        }
    }

    static class TileMapElement
    {
        public static string Name = "TileMap";
        public static string Source = "ImageSource";
        public static string TileWidth = "TileWidth";
        public static string TileHeight = "TileHeight";
        public static string MarginWidth = "MarginWidth";
        public static string MarginHeight = "MarginHeight";
    }

    class Map
    {
        List<Layer> layers;
        int activeLayer = 0;
        public int width, height;

        public Map(int _width,int _height)
        {
            width = _width;
            height = _height;
            layers = new List<Layer>();
            AddLayer();
        }

        public int GetTile(int x,int y)
        {
            return layers[activeLayer].GetTile(x,y);
        }

        public void AddTile(int tileIndex,int x,int y)
        {
            layers[activeLayer].AddTile(x,y,tileIndex);
        }

        public void AddTileAtLayer(int tileIndex,int x,int y,int layerIndex=0)
        {
            layers[layerIndex].AddTile(x,y,tileIndex);
        }

        public void AddLayer()
        {
            layers.Add(new Layer(width,height));
        }

        public void RemoveLayer(int index)
        {
            layers.RemoveAt(index);
        }
    }

    class Layer
    {
        int[,] tiles;
        bool[,] collision;
        string name;

        public Layer(int width,int height)
        {
            tiles = new int[width, height];
            collision = new bool[width, height];
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y< height; y++)
                {
                    tiles[x, y] = -1;
                    collision[x, y] = false;
                }
            }
        }

        public int GetTile(int x,int y)
        {
            return tiles[x, y];
        }

        public void AddTile(int x,int y,int tileIndex)
        {
            tiles[x, y] = tileIndex;
        }
    }

    class TileMap
    {
        public int tileWidth = 32, tileHeight = 32;
        public int tileCountX, tileCountY;
        public string source;
        public Image imageSource;
        Tile[] tiles;
        public int marginWidth, marginHeight;
        public int tileCount = 0;

        public TileMap(string _source)
        {
            source = _source;
            imageSource = Image.FromFile(source);
        }

        public void Cut(int _tileWidth = 32,int _tileHeight = 32,int _marginWidth = 0,int _marginHeight = 0)
        {
            tileWidth = _tileWidth;
            tileHeight = _tileHeight;
            marginHeight = _marginHeight;
            marginWidth = _marginWidth;

            tileCountX = imageSource.Width / tileWidth;
            tileCountY = imageSource.Height / tileHeight;
            tileCount = tileCountX * tileCountY;
            tiles = new Tile[tileCount];
            for(int x = 0; x < tileCountX; x++)
            {
                for(int y = 0; y < tileCountY; y++)
                {
                    tiles[x*tileCountY+y] = new Tile(
                        x*(tileWidth+marginWidth),
                        y*(tileHeight+marginHeight),
                        tileWidth,
                        tileHeight
                    );
                }
            }
        }

        public Tile GetTile(int index)
        {
            return tiles[index];
        }

        public int GetTileIndexFromXY(int x,int y)
        {
            return x * tileCountY + y;
        }
    }

    class Tile
    {
        static int count = 0;
        int id, x, y, width, height;
        public Tile(int _x,int _y,int _width,int _height)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            id = count;
            count++;
        }

        public RectangleF GetImagePosition()
        {
            return new RectangleF(x,y,width,height);
        }
    }
}
