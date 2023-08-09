public class ZoomService
{
    
    public void ZoomIn(IZoomable zoomableComponent)
    {
        zoomableComponent.ZoomIn();
    }

    public void ZoomOut(IZoomable zoomableComponent)
    {
        zoomableComponent.ZoomOut();
    }
}