public class ZoomService
{
    private Control control;

    public void ZoomIn(IZoomable zoomableComponent)
    {
        zoomableComponent.ZoomIn();
    }

    public void ZoomOut(IZoomable zoomableComponent)
    {
        zoomableComponent.ZoomOut();
    }
}