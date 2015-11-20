using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public static class FrameManager
    {
        public static List<Frame> Frames = new List<Frame>() { };

        private static Frame selectedFrame;

        private static Boolean addFrame = false;
        public static void AddFrame()
        {
            addFrame = true;
        }

        public static Boolean ResequenceFrames = false;

        public static Boolean ActiveFrameAltered = false;

        public static void Update()
        {
            if (addFrame)
                addNewFrame();

            if(ResequenceFrames)
                resequenceFrames();

            if(ActiveFrameAltered)
                updateFramesActiveFrame();
        }

        private static void updateFramesActiveFrame()
        {
            Frames[ActiveFrameIndex] = ActiveFrame;
        }

        private static int ActiveFrameIndex
        {
            get
            {
                int ndx = 0;
                foreach(Frame frame in Frames)
                {
                    if (frame.IsActive)
                        break;

                    ndx++;
                }

                return ndx;
            }
        }

        public static Frame ActiveFrame
        {
            get
            {
                return getActiveFrame();
            }
            set
            {
                ActiveFrame.IsActive = false;
                foreach(Frame frame in Frames)
                {
                    if(frame.Sequence == value.Sequence)
                    {
                        frame.IsActive = true;
                        break;
                    }
                }
            }
        }

        private static Frame getActiveFrame()
        {
            
            foreach (Frame frame in Frames)
            {
                if (frame.IsActive)
                {
                    selectedFrame = frame;
                    break;
                }
            }
            return selectedFrame;
        }

        public static void addNewFrame()
        {
            Frame newFrame = new Frame();
            if (Frames.Count > 0)
                newFrame.Sequence = Frames[Frames.Count - 1].Sequence + 1;
            else
                newFrame.Sequence = 1;

            newFrame.Time = 100;
            Grid grid = new Grid(new Vector2(0, 0), Globals.ImageSize);
            newFrame.Grid.Push(grid);
            selectedFrame = newFrame;

            Frames.Add(newFrame);
            ActiveFrame = newFrame;
            addFrame = false;
        }

        private static void resequenceFrames()
        {
            List<Frame> tempList = new List<Frame>();
            
            for(int i = 0; i < Frames.Count; i++)
            {
                foreach(Frame frame in Frames)
                {
                    if(frame.Sequence == i + 1)
                    {
                        tempList.Add(frame);
                        break;
                    }
                }
            }

            Frames = tempList;
        }
    }
}
