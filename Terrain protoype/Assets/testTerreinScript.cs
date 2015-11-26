using UnityEngine;
using System.Collections;

public class testTerreinScript : MonoBehaviour {
	public Terrain terrain;
	public TerrainData terraindata;
	public int levelsizex = 400;
	public int levelsizez = 400;
	void generateTerrain(){
		terraindata = terrain.terrainData;
		int heightmapx = terraindata.heightmapWidth;
		int heightmapz = terraindata.heightmapHeight;
		float terrainsizex = terraindata.size.x;
		float terrainsizez = terraindata.size.z;
		float verhoudingx = levelsizex / terrainsizex;
		float verhoudingz = levelsizez / terrainsizez;
		float beginx = 0.5 * (float)terrainsizex - 0.5 * (float)levelsizex;
		float eindx  = 0.5 * (float)terrainsizex + 0.5 * (float)levelsizex;
		float beginz = 0.5 * (float)terrainsizez - 0.5 * (float)levelsizez;
		float eindz  = 0.5 * (float)terrainsizez + 0.5 * (float)levelsizez;
		int hmlevellengthx = (int)(heightmapx * verhoudingx);
		int hmlevellengthz = (int)(heightmapz * verhoudingz);
		int hmlevelbeginx  = (int)(0.5 * heightmapx - 0.5 * hmlevellengthx);
		int hmleveleindx   = (int)(0.5 * heightmapx + 0.5 * hmlevellengthx);
		int hmlevelbeginz  = (int)(0.5 * heightmapx - 0.5 * hmlevellengthz);
		int hmleveleindz   = (int)(0.5 * heightmapx + 0.5 * hmlevellengthz);

		GameObject hekPrefab = Resources.Load ("Hekje") as GameObject;
		float lengteHek = hekPrefab.GetComponent<Renderer>().bounds.size.x;
		int aantalhekjesx = (int)(levelsizex / lengteHek);
		int aantalhekjesz = (int)(levelsizez / lengteHek);
		for (int i = 0; i < aantalhekjesx; i++) {
			GameObject hek = Instantiate(hekPrefab);
			hek.transform.position = new Vector3 (beginx + i * lengteHek, terrain.transform.position.y, 0);
			GameObject hek2 = Instantiate(hekPrefab);
			hek2.transform.position = new Vector3 (beginx + i * lengteHek, terrain.transform.position.y, eindz);
		}
		for (int i = 0; i < aantalhekjesz; i++) {
			GameObject hek = Instantiate(hekPrefab);
			hek.transform.position = new Vector3 (beginx, terrain.transform.position.y, beginz+ i*lengteHek);
			hek.transform.eulerAngles = new Vector3(0,90,0);
			GameObject hek2 = Instantiate(hekPrefab);
			hek2.transform.position = new Vector3 (eindx, terrain.transform.position.y, beginz + i*lengteHek);
			hek2.transform.eulerAngles = new Vector3(0,90,0);
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
