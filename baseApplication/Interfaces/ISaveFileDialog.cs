public interface ISaveFileDialog
{
    DialogResult ShowDialog();
    string FileName { get; set; }
    string Filter { get; set; }
    string Title { get; set; }
}