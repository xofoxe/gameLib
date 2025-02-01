using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Animation
    {
        Stopwatch sw = new Stopwatch();
        Stopwatch timer = new Stopwatch();
        public Bitmap CurrentImage { get; private set; }
        private bool animationTrigger;
        public int time_prev;
        private int time_now_reload;
        public int time_prev_reload;
        public int AnimTime = 200;
        public Animation()
        {
            sw.Start();
            time_now_reload = 0;
            time_prev_reload = 0;
        }
        public int num = 0;
        public bool checkReload(Weapon weapon)
        {
            time_now_reload = (int)sw.ElapsedMilliseconds;
            if ((time_now_reload - time_prev_reload) > weapon.FireRate)
            {
                time_prev_reload = (int)sw.ElapsedMilliseconds;
                return true;
            }
            return false;
        }
        public void animate(Mob mob, string path)
        {
            if (animationTrigger)
            {
                string[] pngFiles = Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly);
                int count = pngFiles.Length;
                num = (num + 1) % count;
                string p = path + $"{num}" + ".png";
                mob.Sprite = new Bitmap(p);
            }
        }
        public void animateGun(Weapon weapon, string path, int frame)
        {
            checkReload(weapon);
            string p = path + $"{frame}" + ".png";
            weapon.sprite = new Bitmap(p);
        }
        public void checkAnimTime()
        {
            animationTrigger = false;
            int time_now = (int)sw.ElapsedMilliseconds;
            if ((time_now - time_prev) > AnimTime)
            {
                time_prev = (int)sw.ElapsedMilliseconds;
                animationTrigger = true;
            }
        }
        public bool AnimationTrigger
        {
            get => animationTrigger;
            set => animationTrigger = value;
        }
    }
}
