using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace MuneoCrepe.Extensions
{
    public static class DOTweenExtension
    {
        public static async UniTask WaitAsync(this Tween tween,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await UniTask.WaitWhile(tween.IsActive, cancellationToken: cancellationToken);
        }

        public static async UniTask WaitAsync(this Sequence sequence,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await UniTask.WaitWhile(sequence.IsActive, cancellationToken: cancellationToken);
        }
    }
}