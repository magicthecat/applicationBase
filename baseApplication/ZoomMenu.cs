using System.Windows.Forms;

namespace baseApplication
{
    public class ZoomMenu : IMenu
    {
        private IZoomable zoomableComponent;
        private ZoomService zoomService;

        public ZoomMenu(IZoomable zoomableComponent, ZoomService zoomService)
        {
            this.zoomableComponent = zoomableComponent;
            this.zoomService = zoomService;
        }

        public ToolStripMenuItem GenerateMenu()
        {
            ToolStripMenuItem zoomMenu = new ToolStripMenuItem("Zoom");

            ToolStripMenuItem zoomInMenuItem = new ToolStripMenuItem("Zoom In");
            zoomInMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Add; // Shortcut Ctrl + '+'
            zoomInMenuItem.Click += ZoomInMenuItem_Click;

            ToolStripMenuItem zoomOutMenuItem = new ToolStripMenuItem("Zoom Out");
            zoomOutMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Subtract; // Shortcut Ctrl + '-'
            zoomOutMenuItem.Click += ZoomOutMenuItem_Click;

            zoomMenu.DropDownItems.Add(zoomInMenuItem);
            zoomMenu.DropDownItems.Add(zoomOutMenuItem);

            return zoomMenu;
        }

        private void ZoomInMenuItem_Click(object sender, EventArgs e)
        {
            zoomService.ZoomIn(zoomableComponent);
        }

        private void ZoomOutMenuItem_Click(object sender, EventArgs e)
        {
            zoomService.ZoomOut(zoomableComponent);
        }
    }
}