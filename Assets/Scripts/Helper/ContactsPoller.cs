using UnityEngine;

public class ContactsPoller : IUpdate
{
    private float _collisionThreshhold = 0.1f;
    private int _contactsCount = 10;
    private ContactPoint2D[] _contacts;
    private Collider2D _collider;

    public bool IsGrounded { get; private set; }
    public bool HasLeftContacts { get; private set; }
    public bool HasRightContacts { get; private set; }

    public ContactsPoller(Collider2D collider2D)
    {
        _collider = collider2D;
        _contacts = new ContactPoint2D[_contactsCount];
    }

    public void Update()
    {
        IsGrounded = false;
        HasLeftContacts = false;
        HasRightContacts = false;

        _contactsCount = _collider.GetContacts(_contacts);
        for (int i = 0; i < _contactsCount; i++)
        {
            var normal = _contacts[i].normal;

            if (normal.y > _collisionThreshhold)
            {
                IsGrounded = true;
            }
            if (normal.x > _collisionThreshhold)
            {
                HasLeftContacts = true;
            }
            if (normal.x < -_collisionThreshhold)
            {
                HasRightContacts = true;
            }
        }
    }
}