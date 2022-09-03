namespace Game.Code.Unity.Common
{
	using System;

	public interface ILaserDamageableView
	{
		event Action LaserHit;
		void ApplyLaserDamage();
	}
}