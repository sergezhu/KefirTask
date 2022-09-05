namespace Game.Code.View.Common
{
	using System;

	public interface ILaserDamageableView
	{
		event Action LaserHit;
		void ApplyLaserDamage();
	}
}