using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using ProgrammCreator.Domain.Enities;
using ProgrammCreator.Domain.Services.Contracts;

namespace ProgrammCreator.Domain.Services.Images.Generators
{
    public class TablePictureGenerator : IPictureGenerator
    {
        private Bitmap _imgBitmap;
        private int _margin;
        private int _rowHeight;
        private int _borderSize;

        public TablePictureGenerator() : this(new Bitmap(700, 1))
        {
        }

        public TablePictureGenerator(Bitmap imgBitmap)
        {
            _imgBitmap = imgBitmap;
            _margin = 15;
            _rowHeight = 30;
            _borderSize = 2;
        }
        
        public void CreateBody(ICollection<TvShow> programs)
        {
            int programsCount = programs.Count;
            if (_imgBitmap == null) return;
            // It's and body width to
            int currentWidth = _imgBitmap.Width;
            // Start position for drawing body
            int currentHeight = _imgBitmap.Height;
            int bodyHeight = programsCount * _rowHeight + _borderSize;
            // Create new empty bitmap
            _imgBitmap = new Bitmap(_imgBitmap, new Size(currentWidth, currentHeight + bodyHeight));
            var graphics = GetGraphicsFromCurrentBitmap();
            graphics.Clear(Color.White);
            // Some graphics params
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            var pen = new Pen(Color.Black, _borderSize);
            graphics.DrawRectangle(pen, 
                0, currentHeight, currentWidth, bodyHeight);
            var font = new Font("Arial", 24, FontStyle.Regular, GraphicsUnit.Pixel);
            var brush = new SolidBrush(Color.Black);
            int rowY = currentHeight - _borderSize;
            // Draw the 
            foreach (var program in programs)
            {
                graphics.DrawString(program.StartTime, font, brush, _margin, rowY);
                graphics.DrawString(program.Title, font, brush, _margin + 150, rowY);
                int lineY = rowY + _borderSize;
                graphics.DrawLine(pen, 0, lineY, currentWidth, lineY);
                rowY += _rowHeight;
            }
            graphics.Flush();
        }

        public void CreateBody(TvProgram tvProgram)
        {
            CreateBody(tvProgram.Programs);
        }

        private Graphics GetGraphicsFromCurrentBitmap()
        {
            return Graphics.FromImage(_imgBitmap);
        }

        public Bitmap GetResult()
        {
            return _imgBitmap;
        }
    }
}
