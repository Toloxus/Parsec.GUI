namespace Parsec.Core.WindowFactory
{
    public interface IWindowFactory
    {
        /// <summary>
        /// Creates new window.
        /// </summary>        
        void CreateWindow<T>() where T : IWindow;
    }
}
