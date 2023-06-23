using System.Threading;
using Cysharp.Threading.Tasks;

namespace UISystem.Logic
{
    /// <summary>
    /// Асинхронные операции с интерфейсом
    /// </summary>
    public class AsyncUtils
    {
        public static async UniTask WaitForCompleted(IAwaitable awaitable, CancellationToken cancellationToken)
        {
            if(awaitable.IsCompleted)
                return;

            while (!awaitable.IsCompleted)
            {
                await UniTask.Yield(cancellationToken).SuppressCancellationThrow();
                if(cancellationToken.IsCancellationRequested) return;
            } 
        }
    }
}