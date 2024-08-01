using Cysharp.Threading.Tasks;

public interface IHUDSpawner
{
    public UniTask<HUD> SpawnHUD();
}