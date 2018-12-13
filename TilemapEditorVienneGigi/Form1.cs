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
        //Tilemap courante
        TileMap tileMap;

        //Map courante
        Map map;

        //Nombre de colonne et de lignes dans la tilemap courante
        int columnCount, rowCount;

        //Tile sur laquelle est la souris sur la tilemap
        int currentRow, currentColumn;

        //Tile actuellement selectionnee
        int currentTile;

        //Tile actuelle sur laquelle se trouve la souris dans la map
        int currentMapRow, currentMapColumn;

        bool isMouseDown = false;

        bool isErasing = false;

        //Contexte graphique des deux affichage de tilemap
        Graphics mapG, tileG;

        public Form1()
        {
            InitializeComponent();
        }

        //Permet de selectionner une image dans les fichiers qui va servir de tilemap
        private void LoadTileMap_Click(object sender, EventArgs e)
        {
            //Ouvre l'explorateur de fichiers
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "c:\\";
            fileDialog.Filter = "Images (*.png *.jpg)|*.png;*.jpg";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                map = null;
                tileMap = new TileMap(fileDialog.FileName);
                DisplayTileMapProperties();
            }
        }

        //Dessine les tiles
        private void DisplayTilemap()
        {
            tileG.Clear(Color.White);
            Image img = tileMap.imageSource;
            int cpt = 0;
            for (int x = 0; x < columnCount; x++)
            {
                for(int y = 0; y < rowCount; y++)
                {
                    if (cpt < tileMap.tileCount)
                    {
                        //Affiche chaque tile contenue dans la tilemap
                        tileG.DrawImage(img,
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

        //Dessine la map
        private void DisplayMap()
        {
            mapG.Clear(Color.White);
            for(int x = 0; x <map.width; x++) {
                for(int y = 0; y < map.height; y++)
                {
                    if (map.GetTile(x, y)>-1)
                    {
                        //Dessine chaque tile dans la map
                        mapG.DrawImage(tileMap.imageSource,
                            x * tileMap.tileWidth,
                            y * tileMap.tileHeight,
                            tileMap.GetTile(map.GetTile(x, y)).GetImagePosition(),
                            GraphicsUnit.Pixel
                        );
                    }
                }
            }
        }

        //Met en premier plan la fenêtre pour entrer les propriétés de l'image (Tilemap)
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
            if (tileMap != null)
            {
                XMLHandler.SaveMapAsTML(tileMap, map);
            }
        }

        private void LoadMap_Click(object sender, EventArgs e)
        {
            TileMap t = tileMap;
            Map m = map;
            //Ouvre l'explorateur de fichier pour sauvegarder
            if(XMLHandler.LoadMapFromTML(out tileMap,out map))
            {
                //Initialise les canvas pour afficher les tiles
                columnCount = tileMap.tileCountX;
                rowCount = tileMap.tileCountY;
                TileCanvas.Width = columnCount * (tileMap.tileWidth + 1) + 1;
                TileCanvas.Height = rowCount * (tileMap.tileHeight + 1) + 1;
                DisplayTilemap();
            }
            else
            {
                tileMap = t;
                map = m;
            }
        }

        //Enregistre l'index de la tile selectionnee par l'utilisateur
        private void TileCanvas_Click(object sender, EventArgs e)
        {
            if (tileMap != null)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                //convertis la position de la souris en position dans la tilemap
                currentColumn = (me.X / (tileMap.tileWidth + 1));
                currentRow = (me.Y / (tileMap.tileHeight + 1));
                currentTile = tileMap.GetTileIndexFromXY(currentColumn, currentRow);
            }
        }

        //Insere l'index de la tile courante dans la map, a l'endroit selectionne par l'utilisateur
        private void MapCanvas_Click(object sender, EventArgs e)
        {
            if (map!=null&&tileMap != null&&currentTile!=-1)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                int x = me.X / (tileMap.tileWidth);
                int y = me.Y / (tileMap.tileHeight);
                if (!isErasing)
                {
                    map.AddTile(currentTile, x, y);
                    DisplayMap();
                }
                else
                {
                    //-1 est la valeur nulle pour les tiles
                    map.AddTile(-1, x, y);
                    DisplayMap();
                }   
            }
        }

        private void MapCanvas_MouseDown(object sender, MouseEventArgs me)
        {
            isMouseDown = true;
            if (map!=null && tileMap != null && currentTile != -1)
            {
                //insere une tile dans la map
                int x = me.X / (tileMap.tileWidth);
                int y = me.Y / (tileMap.tileHeight);
                if (!isErasing)
                {
                    map.AddTile(currentTile, x, y);
                    DisplayMap();
                }
                else
                {
                    map.AddTile(-1, x, y);
                    DisplayMap();
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        //Si le clic de la souris est appuye, dessine des tiles la ou l'utilisateur passe sa souris
        private void MapCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (map!=null&&tileMap != null && currentTile != -1)
            {
                int previousMapColumn = currentMapColumn;
                int previousMapRow = currentMapRow;
                currentMapColumn = e.X / (tileMap.tileWidth);
                currentMapRow = e.Y / (tileMap.tileHeight);
                //Verifie si l'utilisateur a changé de position dans la map
                if (previousMapColumn != currentMapColumn || previousMapRow != currentMapRow)
                {
                    if (isMouseDown)
                    {
                        if (!isErasing)
                        {
                            map.AddTile(currentTile, currentMapColumn, currentMapRow);
                            DisplayMap();
                        }
                        else
                        {
                            map.AddTile(-1, currentMapColumn, currentMapRow);
                            DisplayMap();
                        }
                    }
                    else
                    {
                        DisplayMap();
                        mapG.DrawImage(
                            tileMap.imageSource,
                            currentMapColumn*tileMap.tileWidth,
                            currentMapRow*tileMap.tileHeight,
                            tileMap.GetTile(currentTile).GetImagePosition(),
                            GraphicsUnit.Pixel
                            );
                    }
                }
            }
        }

        //Initialise les modes de rendus des canvas
        private void Form1_Load(object sender, EventArgs e)
        {
            mapG = MapCanvas.CreateGraphics();
            tileG = TileCanvas.CreateGraphics();
            mapG.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            mapG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            tileG.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            tileG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        }

        //Bascule entre le mode effacer ou dessiner
        private void Erase_Click(object sender, EventArgs e)
        {
            if (isErasing)
            {
                isErasing = false;
                Erase.Text = "Effacer";
            }
            else
            {
                isErasing = true;
                Erase.Text = "Dessiner";
            }
        }

        private void MapCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        //Ferme la fenêtre des propriétés et les enregistres dans la tilemap
        private void ValidateTileMapProp_Click(object sender, EventArgs e)
        {
            PanelProperties.Visible = false;
            PanelProperties.SendToBack();
            //Coupe l'image chargee en tilemap
            tileMap.Cut((int)TileWidth.Value, (int)TileHeight.Value, (int)MarginWidth.Value,(int)MarginHeight.Value);
            columnCount = tileMap.tileCountX;
            rowCount = tileMap.tileCountY;
            TileCanvas.Width = columnCount * (tileMap.tileWidth + 1) + 1;
            TileCanvas.Height = rowCount * (tileMap.tileHeight + 1) + 1;
            //initialise la map, en lui donnant une taille maximum
            map = new Map(MapCanvas.Width/tileMap.tileWidth,MapCanvas.Height/tileMap.tileHeight);
            DisplayTilemap();
            DisplayMap();
        }

        //récupère la position x et y de la souris + dessine un contour du carré où se trouve la souris
        private void TileCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (tileMap != null&&map!=null)
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
                    tileG.DrawRectangle(new Pen(Color.Gray, 2), x, y, tileMap.tileWidth, tileMap.tileHeight);
                }
            }
        }
    }

    //Objet servant a charger et sauvegarder les map via un fichier .tml
    static class XMLHandler
    {
        //Enregistre une map en .tml
        public static void SaveMapAsTML(TileMap tileMap = null, Map map=null)
        {
            //Ouvre l'explorateur de fichiers
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Map file (*.tml)|*.tml";
            saveFile.Title = "Save the map";
            saveFile.ShowDialog();

            if (saveFile.FileName != "")
            {
                //Cree un document .tml et enregistre les valeurs de la map et de la tilemap qui la compose dedans
                XmlTextWriter textWriter = new XmlTextWriter(saveFile.FileName,Encoding.UTF8);
                textWriter.WriteStartDocument();

                textWriter.WriteStartElement("root");
                    SaveTilemap(ref textWriter, tileMap);

                    SaveMap(ref textWriter, map);
                textWriter.WriteEndElement();

                textWriter.WriteEndDocument();
                textWriter.Close();
            }
        }

        //Enregistre les proprietes de la tilemap
        static void SaveTilemap(ref XmlTextWriter textWriter, TileMap tileMap)
        {
            textWriter.WriteStartElement(TileMapElement.Name);

                textWriter.WriteElementString(TileMapElement.Source,tileMap.source);

                textWriter.WriteElementString(TileMapElement.MarginWidth,tileMap.marginWidth.ToString());

                textWriter.WriteElementString(TileMapElement.MarginHeight, tileMap.marginHeight.ToString());

                textWriter.WriteElementString(TileMapElement.TileWidth, tileMap.tileWidth.ToString());

                textWriter.WriteElementString(TileMapElement.TileHeight, tileMap.tileHeight.ToString());

            textWriter.WriteEndElement();
        }

        //Enregistre les proprietes de la map
        static void SaveMap(ref XmlTextWriter w,Map m)
        {
            w.WriteStartElement(MapElement.Name);
                w.WriteElementString(MapElement.Width, m.width.ToString());

                w.WriteElementString(MapElement.Height, m.height.ToString());

                foreach (Layer l in m.layers)
                {
                    SaveLayer(ref w,l);
                }

            w.WriteEndElement();
        }

        //Enregistre les proprietes d'un layer de la map, ainsi que les tiles qui a compose
        static void SaveLayer(ref XmlTextWriter w,Layer l)
        {
            w.WriteStartElement(LayerElement.Name);
                w.WriteStartElement(LayerElement.Tiles);
                for(int x = 0; x < l.tiles.GetLength(0); x++)
                {
                    w.WriteStartElement(LayerElement.Column);
                    for(int y = 0; y < l.tiles.GetLength(1); y++)
                    {
                        w.WriteElementString(LayerElement.Row, l.tiles[x,y].ToString());
                    }
                    w.WriteEndElement();
                }
                w.WriteEndElement();

                w.WriteStartElement(LayerElement.Collision);
                for (int x = 0; x < l.collision.GetLength(0); x++)
                {
                    w.WriteStartElement(LayerElement.Column);
                    for (int y = 0; y < l.collision.GetLength(1); y++)
                    {
                        w.WriteElementString(LayerElement.Row, l.collision[x, y].ToString());
                    }
                    w.WriteEndElement();
                }
                w.WriteEndElement();
            w.WriteEndElement();
        }

        //Charge un fichier .tml et recree la tilemap et la map a partir de celui-ci
        public static bool LoadMapFromTML(out TileMap t,out Map m)
        {
            //Charge une map en .tml
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "c:\\";
            fileDialog.Filter = "Map files (*.tml)|*.tml";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileDialog.FileName);
                XmlNode root = xmlDoc.SelectSingleNode("root");
                //Cree la tilemap contenue dans le fichier .tml
                XmlNode tileMapNode = root.SelectSingleNode(TileMapElement.Name);
                t = LoadTilemap(tileMapNode);
                //Cree la map contenue dans le fichier .tml associee a la tilemap
                XmlNode mapNode = root.SelectSingleNode(MapElement.Name);
                m = LoadMap(mapNode);
                return true;
            }
            else
            {
                t = null;
                m = null;
                return false;
            }
        }

        //Cree une tilemap a partir du fichier .tml
        static TileMap LoadTilemap(XmlNode node)
        {
            string source = node.SelectSingleNode(TileMapElement.Source).InnerText;
            TileMap t = new TileMap(source);
            int tileWidth = int.Parse(node.SelectSingleNode(TileMapElement.TileWidth).InnerText);
            int tileHeight = int.Parse(node.SelectSingleNode(TileMapElement.TileHeight).InnerText);
            int marginWidth = int.Parse(node.SelectSingleNode(TileMapElement.MarginWidth).InnerText);
            int marginHeight = int.Parse(node.SelectSingleNode(TileMapElement.MarginHeight).InnerText);
            t.Cut(tileWidth, tileHeight, marginWidth, marginHeight);
            return t;
        }

        //cree une Map a partir du fichier .tml
        static Map LoadMap(XmlNode node)
        {
            int width = int.Parse(node.SelectSingleNode(MapElement.Width).InnerText);
            int height = int.Parse(node.SelectSingleNode(MapElement.Height).InnerText);
            Map m = new Map(width,height);
            XmlNodeList list = node.SelectNodes(LayerElement.Name);
            int i = 0;
            foreach(XmlNode n in list)
            {
                if (m.layers.Count < i)
                    m.layers.Add(LoadLayer(n,width,height));
                else
                    m.layers[i] = LoadLayer(n,width,height);
                i++;
            }
            return m;
        }

        //cree un layer a partir du fichier .tml
        static Layer LoadLayer(XmlNode node,int w,int h)
        {
            Layer l = new Layer(w,h);
            XmlNode tiles = node.SelectSingleNode(LayerElement.Tiles);
            XmlNodeList c = tiles.SelectNodes(LayerElement.Column);
            int x = 0;
            foreach(XmlNode n in c)
            {
                int y = 0;
                XmlNodeList r = n.SelectNodes(LayerElement.Row);
                foreach(XmlNode ny in n)
                {
                    l.AddTile(x, y, int.Parse(ny.InnerText));
                    y++;
                }
                x++;
            }

            XmlNode collision = node.SelectSingleNode(LayerElement.Tiles);
            c = collision.SelectNodes(LayerElement.Column);
            x = 0;
            foreach (XmlNode n in c)
            {
                int y = 0;
                XmlNodeList r = n.SelectNodes(LayerElement.Row);
                foreach (XmlNode ny in n)
                {
                    l.AddTile(x, y, int.Parse(ny.InnerText));
                    y++;
                }
                x++;
            }
            return l;
        }
    }

    //Contrat contenant les noms des balises des tilemap
    static class TileMapElement
    {
        public static string Name = "TileMap";
        public static string Source = "ImageSource";
        public static string TileWidth = "TileWidth";
        public static string TileHeight = "TileHeight";
        public static string MarginWidth = "MarginWidth";
        public static string MarginHeight = "MarginHeight";
    }

    //Contrat contenant les noms des balises des map
    static class MapElement
    {
        public static string Name = "Map";
        public static string Width = "Width";
        public static string Height = "Height";
        public static string Layers = "Layers";
    }

    //Contrat contenant les noms des balises des layer
    static class LayerElement
    {
        public static string Name = "Layer";
        public static string Tiles = "Tiles";
        public static string Collision = "Collision";
        public static string Column = "C";
        public static string Row = "R";
    }

    //Classe contenant les différents layers composant une map
    class Map
    {
        public List<Layer> layers;
        int activeLayer = 0;
        public int width, height;

        public Map(int _width,int _height)
        {
            width = _width;
            height = _height;
            layers = new List<Layer>();
            AddLayer();
        }

        //Récupère la position d'une tile
        public int GetTile(int x,int y)
        {
            return layers[activeLayer].GetTile(x,y);
        }

        //Ajoute l'index dans le tableau à une position donnée
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

    //Classe contenant un tableau d'index correspondant a des tiles
    class Layer
    {
        public int[,] tiles;
        public bool[,] collision;

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
            if(x>=0&&x<tiles.GetLength(0)&&y>=0&&y<tiles.GetLength(1))
                tiles[x, y] = tileIndex;
        }
    }

    //Classe contenant l'ensemble des tiles composant une tilemap,l'image source, ainsi que les proprietes communes de ces tiles
    //(longueur, largeur, nombre, etc)
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

        //Découpe l'image donnée en tiles selon la taille entrée par l'utilisateur
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
            if (index >= 0 && index < tiles.Length)
                return tiles[index];
            else
                return tiles[0];
        }

        public int GetTileIndexFromXY(int x,int y)
        {
            return x * tileCountY + y;
        }
    }

    //Classe contenant les positions et taille d'une tile précise (correspondant a des positions dans l'image source
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
