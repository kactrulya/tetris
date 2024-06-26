﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris22
{
    abstract class Figure
    {
        const int LENGHT = 4;
        public Point[] Points = new Point[LENGHT];

        public void Draw()
        {
            foreach (Point p in Points)
            {
                p.Draw();
            }
        }

        internal Result TryMove(Direction dir)
        {
            Hide();
            Move(dir);

            var result = VerifyPosition();
            if (result != Result.SUCCESS)
                Move(Reverse(dir));

            Draw();
            return result;
        }

        private Direction Reverse(Direction dir)
        {
            switch (dir)
            {
                case Direction.LEFT:
                    return Direction.RIGHT;
                case Direction.RIGHT:
                    return Direction.LEFT;
                case Direction.DOWN:
                    return Direction.UP;
                case Direction.UP:
                    return Direction.DOWN;
            }
            return dir;
        }

        internal Result TryRotate()
        {
            Hide();
            Rotate();

            var result = VerifyPosition();
            if (result != Result.SUCCESS)
                Rotate();

            Draw();
            return result;
        }

        private Result VerifyPosition()
        {
            foreach(var p in Points)
            {
                if(p.Y >= Field.Height)
                    return Result.DOWN_BORDER_STRIKE;
                if (p.X >= Field.Width || p.X < 0 || p.Y < 0)
                    return Result.BORDER_STRIKE;
                if (Field.CheckStrike(p))
                    return Result.HEAP_STRIKE;
            }
            
            return Result.SUCCESS;
        }

        public void Move(Direction dir)
        {
            foreach(var p in Points) 
            {
                p.Move(dir);
            }
        }

       /* public void Move(Direction dir)
        {
            Hide();
            foreach (Point p in points)
            {
                p.Move(dir);
            }
            Draw();
        }*/

        public void Hide()
        {
            foreach(Point p in Points)
            {
                p.Hide();
            }
        }

        public abstract void Rotate();

        internal bool IsOnTop()
        {
            return Points[0].Y == 0;
        }
    }
}
