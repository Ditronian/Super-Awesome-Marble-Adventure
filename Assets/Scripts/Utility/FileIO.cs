using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class FileIO
    {
        private static string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "player.xml");

        public static PlayerIO loadPlayer()
        {
            PlayerIO player;

            if (File.Exists(filepath))
            {
                try
                {
                    //Prepare for Deserialization
                    XmlSerializer reader = new XmlSerializer(typeof(PlayerIO));
                    StreamReader file = new StreamReader(filepath);

                    //Deserialize from XML
                    player = (PlayerIO)reader.Deserialize(file);
                    file.Close();
                    //Debug.Log("Read: " + player.SkinChoice);
                }
                catch
                {
                    player = new PlayerIO();
                }
                
            }
            else player = new PlayerIO();

            return player;
        }

        //Serializes the Player's data to XML
        public static void savePlayer(PlayerIO player)
        {
            //Prepare for Serialization
            XmlSerializer writer = new XmlSerializer(typeof(PlayerIO));
            FileStream file = File.Create(filepath);

            //Serialize to XML
            writer.Serialize(file, player);
            file.Close();
            //Debug.Log("Wrote: "+ player.SkinChoice + "to " +filepath);
        }
    }
}
