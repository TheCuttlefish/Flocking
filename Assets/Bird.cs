using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Bird : MonoBehaviour {


	private float thrust= 3.0f;
	int id;
	private Rigidbody rb;
	public List<GameObject> boids = new List<GameObject>();

	float y;//ypos

	float xRot;
	float yRot;
	float zRot;
	Vector3 center = Vector3.zero;


	float dt;//delta time

	void Start () {
		PopulateList ();
		rb = GetComponent<Rigidbody>();
	}



	void FixedUpdate () {
	//delta time
		dt = Time.deltaTime;
	//positions
		y = transform.position.y;
	//rotations
		xRot = transform.localRotation.x;
		yRot = transform.localRotation.y;
		zRot = -transform.localRotation.z*10.0f;

		float distFromCenter = Vector3.Distance(center, transform.position);

		ShowAxes();

		if (distFromCenter > 10.0f) {
			SetColour ( Color.red);

			Vector3 targetDir = center - transform.position;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 2.0f*dt, dt*20);

			Debug.DrawLine(transform.position, Vector3.zero, Color.white);


			transform.rotation = Quaternion.LookRotation(newDir);


		} else {
			SetColour (Color.white);
		}


		transform.Translate(Vector3.forward * dt *thrust);
		transform.Rotate (-xRot+y/10, 0, zRot );




		RemoveVelocity ();


		Reset();
	}


	void SetColour(Color c){
		this.GetComponent<Renderer> ().material.color = c;
	}

	void PopulateList(){
		boids = new List<GameObject>(GameObject.FindGameObjectsWithTag("boid"));
		//print ("before " + boids.Count);
		for (int i = 0; i < boids.Count; i++) {
			//remove self
			if (boids [i].transform.position == transform.position) {
				boids.RemoveAt (i);
			}
		}
		//print ("after " + boids.Count);
	}

	void RemoveVelocity(){
		//maybe lerp it...
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero; 
	}

	void ShowAxes(){
		
			Debug.DrawLine (transform.position, transform.position + transform.up, Color.green);
			Debug.DrawLine (transform.position, transform.position + transform.forward, Color.blue);
			Debug.DrawLine (transform.position, transform.position + transform.right, Color.red);
		
	}

	void Reset(){
		if(Input.GetKeyDown(KeyCode.R))Application.LoadLevel(0);

	}

}
