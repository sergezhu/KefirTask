namespace Game.Code.Core.Common
{
	using System;

	public interface ILaserDamageableView
	{
		event Action LaserHit;
		void ApplyLaserDamage();
	}
}