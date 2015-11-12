using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipBook
{
    public static class FrameManager
    {
        public static List<Frame> Frames = new List<Frame>();
        private static List<Frame> addingFrames = new List<Frame>();
        //private static List<Frame> resequencingFrames = new List<Frame>();

        private static Boolean addFrame = false;
        public static void AddFrame()
        {
            addFrame = true;
        }

        public static Boolean ResequenceFrames = false;

        public static void Update()
        {
            if (addFrame)
                addNewFrame();

            if(ResequenceFrames)
                resequenceFrames();
        }

        public static Frame ActiveFrame
        {
            get
            {
                Frame retFrame = new Frame();
                foreach(Frame frame in Frames)
                {
                    if (frame.IsActive)
                    {
                        retFrame = frame;
                        break;
                    }
                }
                return retFrame;
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

        private static void addNewFrame()
        {
            Frame newFrame = new Frame();
            newFrame.Sequence = Frames[Frames.Count - 1].Sequence + 1;
            newFrame.Time = 100;


            Frames.Add(new Frame());
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
