﻿using System;

namespace PainterGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Painter())
                game.Run();
        }
    }
}
