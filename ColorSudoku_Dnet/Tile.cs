using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorSudoku_Dnet
{
    [Serializable]
    public class Tile
    {
            public int Value { get; set; }
            public bool Locked { get; set; }

            public Tile()
            {
                Value = 0;
                Locked = false;
            }

            public String GetImageName()
            {
                switch (Value)
                {
                    case 0:
                        return "0";
                    case 1:
                        if (Locked)
                            return "1b";
                        else
                            return "1";
                    case 2:
                        if (Locked)
                            return "2b";
                        else
                            return "2";
                    case 3:
                        if (Locked)
                            return "3b";
                        else
                            return "3";
                    case 4:
                        if (Locked)
                            return "4b";
                        else
                            return "4";
                    case 5:
                        if (Locked)
                            return "5b";
                        else
                            return "5";
                    case 6:
                        if (Locked)
                            return "6b";
                        else
                            return "6";
                    case 7:
                        if (Locked)
                            return "7b";
                        else
                            return "7";
                    case 8:
                        if (Locked)
                            return "8b";
                        else
                            return "8";
                    case 9:
                        if (Locked)
                            return "9b";
                        else
                            return "9";

                }
                throw  new ArgumentException("Parameter cannot be null");
            }

    }
 }

