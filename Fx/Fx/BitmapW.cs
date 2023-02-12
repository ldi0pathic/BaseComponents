using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Fx
{
    class BitmapW
    {
        readonly LockBitmap holder;
        bool isLocked = false;

        public BitmapW(string filename)
        {
            holder = new LockBitmap(new Bitmap(filename));
            Lock();
        }

        BitmapW(Bitmap a)
        {
            holder = new LockBitmap(a);
            Lock();
        }

        private void Lock()
        {
            if (!isLocked)
            {
                holder.LockBits();
                isLocked = true;
            }
        }
        private void Unlock()
        {
            if (isLocked) 
            { 
                holder.UnlockBits();
                isLocked = false; 
            }
        }

        public void SetPixel(int x, int y, Color color)
        {
            Lock();
            holder.SetPixel(x, y, color);
        }

        public Color GetPixel(int x, int y)
        {
            return holder.GetPixel(x, y);
        }

        public int Width()
        {
            return holder.Width;
        }

        public int Height()
        {
            return holder.Height;
        }
        public Bitmap GetBitmap()
        {
            Unlock();
            return holder.GetBitmap();
        }
        public BitmapW Clone()
        {
            Unlock();
            return new BitmapW((Bitmap)holder.GetBitmap().Clone());
        }

    }
}
