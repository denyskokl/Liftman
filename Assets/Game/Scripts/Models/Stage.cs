using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Stage
{
    public int Number { get; set; }
    public int GuestNumber { get; set; }
    public float Offset { get; set; }


    public Stage(int number)
    {
        Number = number;
    }
}
