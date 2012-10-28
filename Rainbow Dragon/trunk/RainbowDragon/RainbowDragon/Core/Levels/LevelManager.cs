using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using RainbowDragon.Core.Pickups;
using RainbowDragon.Core.Enemies;

namespace RainbowDragon.Core.Levels
{
    class LevelManager
    {
        int currentLevel;
        int currentNumberSuns;
        int currentNumberArrows;
        int maxTime;
        List<Level> levels;

        public LevelManager()
        {



        }


        public void Initialize()
        {

            System.IO.Stream stream = TitleContainer.OpenStream("Content\\Core\\levels.xml");
            XDocument doc = XDocument.Load(stream);
            levels = new List<Level>();
            levels = (from level in doc.Descendants("level")
                      select new Level()
                      {
                          levelNumber = Convert.ToInt32(level.Element("number").Value),
                          time = Convert.ToInt32(level.Element("time").Value),
                          suns = (from sun in level.Descendants("sun")
                                  select new Sun()
                                  {
                                      position = new Vector2(Convert.ToSingle(sun.Element("xposition").Value), Convert.ToSingle(sun.Element("yposition").Value)),
                                      size = Convert.ToInt32(sun.Element("size").Value),
                                      type = sun.Element("type").Value

                                  }).ToList(),
                          arrows = (from arrow in level.Descendants("arrow")
                                    select new Arrow()
                                    {
                                        direction = Convert.ToInt32(arrow.Element("direction").Value),
                                        position = new Vector2(Convert.ToSingle(arrow.Element("xposition").Value), Convert.ToSingle(arrow.Element("yposition").Value)),
                                        type = arrow.Element("type").Value



                                    }).ToList()


                      }).ToList();




        }
    }
}
