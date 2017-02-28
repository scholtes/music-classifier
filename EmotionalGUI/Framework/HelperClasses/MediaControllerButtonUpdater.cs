using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Framework
{
    public enum MAction
    {
        MouseDown,
        MouseUp,
        Hover,
        UnHover
    }

    public enum MediaControl
    {
        Play,
        Pause,
        Next,
        Prev,
        Stop
    }

    public class MediaControllerButtonUpdater
    {
        private Image play;
        private Image next;
        private Image stop;
        private Image prev;
        private Image pause;

        private ControlState playstate;
        private ControlState nextstate;
        private ControlState stopstate;
        private ControlState prevstate;
        private ControlState pausestate;

        public MediaControllerButtonUpdater(Image prevControl, Image playControl, Image pauseControl, Image stopControl,Image nextControl)
        {
            play = playControl;
            next = nextControl;
            pause = pauseControl;
            stop = stopControl;
            prev = prevControl;
            playstate = ControlState.Disabled;
            nextstate = ControlState.Disabled;
            stopstate = ControlState.Disabled;
            prevstate = ControlState.Disabled;
            pausestate = ControlState.Disabled;
        }

        private void SetImage(MediaControl mc, ControlState cs)
        {
            switch (mc)
            {
                case MediaControl.Prev:
                    switch (cs)
                    {
                        case ControlState.Disabled:
                            prev.Source = LoadImage("Previous", "Disabled");
                            prevstate = ControlState.Disabled;
                            break;
                        case ControlState.Hover:
                            prev.Source = LoadImage("Previous", "Hover");
                            prevstate = ControlState.Hover;
                            break;
                        case ControlState.Normal:
                            prev.Source = LoadImage("Previous", "Normal");
                            prevstate = ControlState.Normal;
                            break;
                        case ControlState.Pressed:
                            prev.Source = LoadImage("Previous", "Pressed");
                            prevstate = ControlState.Pressed;
                            break;
                        case ControlState.PressedHover:
                            prev.Source = LoadImage("Previous", "Pressed Hover");
                            prevstate = ControlState.PressedHover;
                            break;
                    }
                    break;
                case MediaControl.Play:
                    switch (cs)
                    {
                        case ControlState.Disabled:
                            play.Source = LoadImage("Play", "Disabled");
                            playstate = ControlState.Disabled;
                            break;
                        case ControlState.Hover:
                            play.Source = LoadImage("Play", "Hover");
                            playstate = ControlState.Hover;
                            break;
                        case ControlState.Normal:
                            play.Source = LoadImage("Play", "Normal");
                            playstate = ControlState.Normal;
                            break;
                        case ControlState.Pressed:
                            play.Source = LoadImage("Play", "Pressed");
                            playstate = ControlState.Pressed;
                            break;
                        case ControlState.PressedHover:
                            play.Source = LoadImage("Play", "Pressed Hover");
                            playstate = ControlState.PressedHover;
                            break;
                    }
                    break;
                case MediaControl.Pause:
                    switch (cs)
                    {
                        case ControlState.Disabled:
                            pause.Source = LoadImage("Pause", "Disabled");
                            pausestate = ControlState.Disabled;
                            break;
                        case ControlState.Hover:
                            pause.Source = LoadImage("Pause", "Hover");
                            pausestate = ControlState.Hover;
                            break;
                        case ControlState.Normal:
                            pause.Source = LoadImage("Pause", "Normal");
                            pausestate = ControlState.Normal;
                            break;
                        case ControlState.Pressed:
                            pause.Source = LoadImage("Pause", "Pressed");
                            pausestate = ControlState.Pressed;
                            break;
                        case ControlState.PressedHover:
                            pause.Source = LoadImage("Pause", "Pressed Hover");
                            pausestate = ControlState.PressedHover;
                            break;
                    }
                    break;
                case MediaControl.Stop:
                    switch (cs)
                    {
                        case ControlState.Disabled:
                            stop.Source = LoadImage("Stop", "Disabled");
                            stopstate = ControlState.Disabled;
                            break;
                        case ControlState.Hover:
                            stop.Source = LoadImage("Stop", "Hover");
                            stopstate = ControlState.Hover;
                            break;
                        case ControlState.Normal:
                            stop.Source = LoadImage("Stop", "Normal");
                            stopstate = ControlState.Normal;
                            break;
                        case ControlState.Pressed:
                            stop.Source = LoadImage("Stop", "Pressed");
                            stopstate = ControlState.Pressed;
                            break;
                        case ControlState.PressedHover:
                            stop.Source = LoadImage("Stop", "Pressed Hover");
                            stopstate = ControlState.PressedHover;
                            break;
                    }
                    break;
                case MediaControl.Next:
                    switch (cs)
                    {
                        case ControlState.Disabled:
                            next.Source = LoadImage("Next", "Disabled");
                            nextstate = ControlState.Disabled;
                            break;
                        case ControlState.Hover:
                            next.Source = LoadImage("Next", "Hover");
                            nextstate = ControlState.Hover;
                            break;
                        case ControlState.Normal:
                            next.Source = LoadImage("Next", "Normal");
                            nextstate = ControlState.Normal;
                            break;
                        case ControlState.Pressed:
                            next.Source = LoadImage("Next", "Pressed");
                            nextstate = ControlState.Pressed;
                            break;
                        case ControlState.PressedHover:
                            next.Source = LoadImage("Next", "Pressed Hover");
                            nextstate = ControlState.PressedHover;
                            break;
                    }
                    break;
            }
        }

        public void UpdateImage(MediaControl mc, MAction a)
        {
            switch (mc)
            {
                case MediaControl.Next:
                    if (nextstate == ControlState.Normal && a == MAction.Hover) { SetImage(mc, ControlState.Hover); return; }
                    if (nextstate == ControlState.Hover && a == MAction.UnHover) { SetImage(mc, ControlState.Normal); return; }
                    if (nextstate == ControlState.Hover && a == MAction.MouseDown) { SetImage(mc, ControlState.Pressed); SetImage(MediaControl.Play, ControlState.Pressed); SetImage(MediaControl.Pause, ControlState.Normal); SetImage(MediaControl.Stop, ControlState.Normal); return; }
                    if (nextstate == ControlState.Pressed && a == MAction.Hover) { SetImage(mc, ControlState.PressedHover); return; }
                    if (nextstate == ControlState.PressedHover && a == MAction.MouseUp) { SetImage(mc, ControlState.Pressed); return; }
                    if (nextstate == ControlState.Pressed && a == MAction.MouseUp) { SetImage(mc, ControlState.Hover); return; }
                    break;
                case MediaControl.Prev:
                    if (prevstate == ControlState.Normal && a == MAction.Hover) { SetImage(mc, ControlState.Hover); return; }
                    if (prevstate == ControlState.Hover && a == MAction.UnHover) { SetImage(mc, ControlState.Normal); return; }
                    if (prevstate == ControlState.Hover && a == MAction.MouseDown) { SetImage(mc, ControlState.Pressed); SetImage(MediaControl.Play, ControlState.Pressed); SetImage(MediaControl.Pause, ControlState.Normal); SetImage(MediaControl.Stop, ControlState.Normal); return; }
                    if (prevstate == ControlState.Pressed && a == MAction.Hover) { SetImage(mc, ControlState.PressedHover); return; }
                    if (prevstate == ControlState.PressedHover && a == MAction.MouseUp) { SetImage(mc, ControlState.Pressed); return; }
                    if (prevstate == ControlState.Pressed && a == MAction.MouseUp) { SetImage(mc, ControlState.Hover); return; }
                    break;
                case MediaControl.Pause:
                    if (pausestate == ControlState.Normal && a == MAction.Hover) { SetImage(mc, ControlState.Hover); return; }
                    if (pausestate == ControlState.Hover && a == MAction.UnHover) { SetImage(mc, ControlState.Normal); return; }
                    if (pausestate == ControlState.Hover && a == MAction.MouseDown) { SetImage(mc, ControlState.Pressed); SetImage(MediaControl.Play, ControlState.Normal); return; }
                    //if (pausestate == ControlState.Pressed && a == MAction.Hover) { SetImage(mc, ControlState.PressedHover); return; }
                    //if (pausestate == ControlState.PressedHover && a == MAction.UnHover) { SetImage(mc, ControlState.Pressed); return; }
                    //if (pausestate == ControlState.PressedHover && a == MAction.MouseDown) { SetImage(mc, ControlState.Hover); return; }
                    break;
                case MediaControl.Play:
                    if (playstate == ControlState.Normal && a == MAction.Hover) { SetImage(mc, ControlState.Hover); return; }
                    if (playstate == ControlState.Hover && a == MAction.UnHover) { SetImage(mc, ControlState.Normal); return; }
                    if (playstate == ControlState.Hover && a == MAction.MouseDown) { SetImage(mc, ControlState.PressedHover); SetImage(MediaControl.Pause, ControlState.Normal); SetImage(MediaControl.Stop, ControlState.Normal); return; }
                    if (playstate == ControlState.Pressed && a == MAction.Hover) { SetImage(mc, ControlState.PressedHover); return; }
                    if (playstate == ControlState.PressedHover && a == MAction.UnHover) { SetImage(mc, ControlState.Pressed); return; }
                    if (playstate == ControlState.PressedHover && a == MAction.MouseDown) { SetImage(mc, ControlState.Hover); SetImage(MediaControl.Pause, ControlState.Pressed); return; }
                    break;
                case MediaControl.Stop:
                    if (stopstate == ControlState.Normal && a == MAction.Hover) { SetImage(mc, ControlState.Hover); return; }
                    if (stopstate == ControlState.Hover && a == MAction.UnHover) { SetImage(mc, ControlState.Normal); return; }
                    if (stopstate == ControlState.Hover && a == MAction.MouseDown) { SetImage(mc, ControlState.Pressed); SetImage(MediaControl.Play, ControlState.Normal); SetImage(MediaControl.Pause, ControlState.Pressed); return; }
                    //if (stopstate == ControlState.Pressed && a == MAction.Hover) { SetImage(mc, ControlState.PressedHover); return; }
                    //if (stopstate == ControlState.PressedHover && a == MAction.UnHover) { SetImage(mc, ControlState.Pressed); return; }
                    //if (stopstate == ControlState.PressedHover && a == MAction.MouseDown) { SetImage(mc, ControlState.Hover); return; }
                    break;
            }
        }


        private BitmapImage LoadImage(string control, string state)
        {
            string picturePath = Environment.CurrentDirectory + "/../../Design/Pics/Media Controls/" + state + "/" + control + ".png";
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(picturePath);
            image.EndInit();
            return image;
        }

        public void EnableButtons()
        {
            SetImage(MediaControl.Next, ControlState.Normal);
            SetImage(MediaControl.Play, ControlState.Normal);
            SetImage(MediaControl.Pause, ControlState.Normal);
            SetImage(MediaControl.Stop, ControlState.Normal);
            SetImage(MediaControl.Prev, ControlState.Normal);

            playstate = ControlState.Normal;
            nextstate = ControlState.Normal;
            stopstate = ControlState.Normal;
            pausestate = ControlState.Normal;
            prevstate = ControlState.Normal;
        }

        public void DisableButtons()
        {
            SetImage(MediaControl.Next, ControlState.Disabled);
            SetImage(MediaControl.Play, ControlState.Disabled);
            SetImage(MediaControl.Pause, ControlState.Disabled);
            SetImage(MediaControl.Stop, ControlState.Disabled);
            SetImage(MediaControl.Prev, ControlState.Disabled);

            playstate = ControlState.Disabled;
            nextstate = ControlState.Disabled;
            stopstate = ControlState.Disabled;
            pausestate = ControlState.Disabled;
            prevstate = ControlState.Disabled;
        }

        private enum ControlState
        {
            Pressed,
            Normal,
            Hover,
            PressedHover,
            Disabled
        }
    }
}
