using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMixer
{
    public class UndoRedo
    {
        public Stack<Bitmap> undo;
        public Stack<Bitmap> redo;
        public UndoRedo()
        {
            undo = new Stack<Bitmap>();
            redo = new Stack<Bitmap>();
        }

        public void CommitAction(Bitmap previousState) 
        {
            undo.Push(previousState);
            redo.Clear();
        }
        public Bitmap UndoAction(Bitmap currentState) 
        {
            redo.Push(currentState);
            return undo.Pop();
        }
        public Bitmap RedoAction(Bitmap previousState) 
        {
            undo.Push(previousState);
            return redo.Pop();
        }

        public bool EmptyUndo() 
        {
            return undo.Count == 0;
        }

        public bool EmptyRedo()
        {
            return redo.Count == 0;
        }

        public void Clear()
        {
            undo.Clear();
            redo.Clear();
        }
    }
}
