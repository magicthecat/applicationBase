using System;
using System.Windows.Forms;

namespace baseApplication
{
    public partial class Form1 : Form
    {
        private IDataComponent dataComponent;
        private FileManager fileManager;
        private MenuManager menuManager;
        private IDataComponentFactory dataComponentFactory;
        private ZoomService zoomService;
        private ZoomMenu zoomMenu;

        public Form1()
        {
            InitializeComponent();

           this.dataComponentFactory = new TextBoxDataComponentFactory(this);
        
            InitializeDataComponent();
            InitializeFileManager();
            InitializeZoomService();
            InitializeMenuManager();
            UpdateTitle(); 
        }

      private void InitializeDataComponent()
{
    this.dataComponent = dataComponentFactory.CreateAndSetup();
    this.dataComponent.ContentChanged += DataComponent_ContentChanged;
}

private void DataComponent_ContentChanged(object sender, EventArgs e)
{
        fileManager.MarkAsUnsaved();
    UpdateTitle();
}

private void UpdateTitle()
{
    if (string.IsNullOrEmpty(fileManager.CurrentFileName))
    {
        this.Text = "Untitled*";
    }
    else
    {
        this.Text = fileManager.IsSaved ? fileManager.CurrentFileName : fileManager.CurrentFileName + "*";
    }
}

private void InitializeFileManager()
{
    this.fileManager = new FileManager(new DefaultFileService(), new DefaultDialogService());
    this.fileManager.FileSaved += (sender, e) => UpdateTitle();
    this.fileManager.FileNew += (sender, e) => UpdateTitle();
    this.fileManager.FileOpened += (sender, e) => UpdateTitle();
}

private void InitializeZoomService()
{
    this.zoomService = new ZoomService();

    if(dataComponent is IZoomable)
    {
        this.zoomMenu = new ZoomMenu(dataComponent as IZoomable, zoomService);

           Control underlyingControl = dataComponent as Control;
        if (underlyingControl != null)
        {
            underlyingControl.MouseWheel += (sender, e) =>
            {
                if (e.Delta > 0) 
                {
                    zoomService.ZoomIn(dataComponent as IZoomable);
                }
                else
                {
                    zoomService.ZoomOut(dataComponent as IZoomable);
                }
            };
        }
    }
    else
    {
        throw new Exception("DataComponent does not support zooming. Make sure it implements IZoomable.");
    }
}


     private void InitializeMenuManager()
{
    this.menuManager = new MenuManager(dataComponent, fileManager);

    MenuStrip menuStrip = menuManager.InitializeMenuStrip();

    ExportMenu exportMenu = new ExportMenu(dataComponent);
    menuStrip.Items.Add(exportMenu.GenerateMenu());

    if (zoomMenu != null)
    {
        menuStrip.Items.Add(zoomMenu.GenerateMenu());
    }
    this.MainMenuStrip = menuStrip;
    this.Controls.Add(menuStrip);
}

    }
}