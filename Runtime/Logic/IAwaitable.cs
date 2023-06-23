using Cysharp.Threading.Tasks;

namespace UISystem.Logic
{
    /// <summary>
    /// Интерфейс ожидаемого события
    /// </summary>
    public interface IAwaitable
    {
        public bool IsCompleted { get; }
        public UniTask WaitForCompleted();
    }
}