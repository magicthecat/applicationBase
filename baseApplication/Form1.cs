using System;
using System.Windows.Forms;

namespace baseApplication
{
    public partial class Form1 : Form, ITitle
    {
        private IDataComponent dataComponent;
        private FileManager fileManager;
        private TitleManager titleManager;
        private MenuManager menuManager;
        private SaveFileDialogWrapper saveFileDialogWrapper;
        private IDataComponentFactory dataComponentFactory;
        private ZoomService zoomService;
        private ZoomMenu zoomMenu;
        private ISaveFileDialogFactory saveFileDialogFactory;

        public string Title 
            {
                get => this.Text;
                set => this.Text = value;
            }
          public Form1()
    {
        InitializeComponent();

        this.dataComponentFactory = new TextBoxDataComponentFactory(this);
        this.saveFileDialogFactory = new SaveFileDialogFactory(); 

        InitializeDataComponent();
        InitializeFileManager();
        InitializeZoomService();
        InitializeMenuManager();
        titleManager = new TitleManager(this, fileManager); 
        titleManager.UpdateTitle(); 
    }

      private void InitializeDataComponent()
{
    this.dataComponent = dataComponentFactory.CreateAndSetup();
    this.dataComponent.ContentChanged += DataComponent_ContentChanged;
            this.saveFileDialogWrapper = new SaveFileDialogWrapper(saveFileDialogFactory);
}

private void DataComponent_ContentChanged(object sender, EventArgs e) => fileManager.MarkAsUnsaved();
    

private void InitializeFileManager() => this.fileManager = new FileManager(new DefaultFileService(), new DefaultDialogService());
  

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

    ExportMenu exportMenu = new ExportMenu(dataComponent, saveFileDialogWrapper);  
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