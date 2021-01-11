using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal2DBullet : MonoBehaviour {

    [SerializeField] Rigidbody2D body;
    public Portal2D portal { get; set; }
    public int targetMask { get; set; }
    public float timeout { get; set; }
    public Collider2D last { get; set; }
    public Collider2D original { get; set; }
    public string targetTag { get; set; }
    public bool isLock { get; set; }
    private Vector2 min, max, PMin, point;
    private float maskOffset = .1f; // сдвиг маски,чтобы она не обрезала с края портала
    private int id;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
