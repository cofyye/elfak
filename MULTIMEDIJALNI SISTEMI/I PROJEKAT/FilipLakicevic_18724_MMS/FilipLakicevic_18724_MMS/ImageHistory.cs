using System;
using System.Collections.Generic;
using System.Drawing;

namespace FilipLakicevic_18724_MMS
{
    public class ImageHistory
    {
        private Stack<Bitmap> undoStack;
        private Stack<Bitmap> redoStack;
        private int maxHistorySize;

        public ImageHistory(int maxSize = 50)
        {
            undoStack = new Stack<Bitmap>();
            redoStack = new Stack<Bitmap>();
            maxHistorySize = maxSize;
        }

        public void SaveState(Bitmap imageState)
        {
            // Create a copy of the bitmap to avoid reference issues
            Bitmap copy = new Bitmap(imageState);
            undoStack.Push(copy);
            
            // Clear redo stack when new action is performed
            ClearRedoStack();
            
            // Limit history size to prevent memory issues
            if (undoStack.Count > maxHistorySize)
            {
                var oldestItems = new Bitmap[undoStack.Count - maxHistorySize];
                for (int i = 0; i < oldestItems.Length; i++)
                {
                    oldestItems[i] = undoStack.ElementAt(undoStack.Count - 1 - i);
                }
                
                // Remove oldest items and dispose them
                for (int i = 0; i < oldestItems.Length; i++)
                {
                    var item = undoStack.ElementAt(undoStack.Count - 1);
                    if (item != null)
                    {
                        item.Dispose();
                    }
                }
            }
        }

        public Bitmap PerformUndo(Bitmap currentImage)
        {
            if (!CanUndo()) return null;
            
            // Save current state to redo stack
            Bitmap currentCopy = new Bitmap(currentImage);
            redoStack.Push(currentCopy);
            
            // Return previous state
            return undoStack.Pop();
        }

        public Bitmap PerformRedo(Bitmap currentImage)
        {
            if (!CanRedo()) return null;
            
            // Save current state to undo stack
            Bitmap currentCopy = new Bitmap(currentImage);
            undoStack.Push(currentCopy);
            
            // Return next state
            return redoStack.Pop();
        }

        public bool CanUndo()
        {
            return undoStack.Count > 0;
        }

        public bool CanRedo()
        {
            return redoStack.Count > 0;
        }

        public void ClearHistory()
        {
            ClearUndoStack();
            ClearRedoStack();
        }

        private void ClearUndoStack()
        {
            while (undoStack.Count > 0)
            {
                var bitmap = undoStack.Pop();
                bitmap?.Dispose();
            }
        }

        private void ClearRedoStack()
        {
            while (redoStack.Count > 0)
            {
                var bitmap = redoStack.Pop();
                bitmap?.Dispose();
            }
        }

        public int UndoCount => undoStack.Count;
        public int RedoCount => redoStack.Count;
    }
}