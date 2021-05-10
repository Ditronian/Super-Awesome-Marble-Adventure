using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utility
{
    [Serializable]
    public class PlayerIO
    {
        private int skinChoice = 1;
        private int currentLevel;

        public PlayerIO()
        {
            
        }

        public PlayerIO(int skinChoice = 1)
        {
            this.skinChoice = skinChoice;
        }

        public int SkinChoice
        {
            get
            {
                return skinChoice;
            }

            set
            {
                skinChoice = value;
            }
        }

        public int CurrentLevel
        {
            get
            {
                return currentLevel;
            }

            set
            {
                currentLevel = value;
            }
        }
    }
}
