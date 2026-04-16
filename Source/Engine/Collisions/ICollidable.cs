namespace SapukayaEngine;

public interface ICollidable
{
	public int Layer { get; set; }
	public int Mask { get; set; }

	public Collisor Collisor { get; set; }

	public bool CanCollide { get; set; }

	public void OnCollide(Entity other);

	public bool Collides(ICollidable other);
}
