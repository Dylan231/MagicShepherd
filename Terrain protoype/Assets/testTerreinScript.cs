using UnityEngine;
using System.Collections;

public class testTerreinScript : MonoBehaviour {
	public Terrain terrain;
	public int levelsizex = 100;
	public int levelsizez = 100;
	public int aantalHeuvels;
	public int aantalVijvers;
	public int aantalBergen;
	void generateTerrain(){
		TerrainData terraindata = terrain.terrainData;
		int heightmapx = terraindata.heightmapWidth;
		int heightmapz = terraindata.heightmapHeight;
		float beginhoogte = 0.003f;
		float[,] heights = new float[heightmapx, heightmapz];
		float terrainsizex = terraindata.size.x;
		float terrainsizez = terraindata.size.z;
		float verhoudingx = levelsizex / terrainsizex;
		float verhoudingz = levelsizez / terrainsizez;
		float beginx = 0.5f * (float)terrainsizex - 0.5f * (float)levelsizex;
		float eindx  = 0.5f * (float)terrainsizex + 0.5f * (float)levelsizex;
		float beginz = 0.5f * (float)terrainsizez - 0.5f * (float)levelsizez;
		float eindz  = 0.5f * (float)terrainsizez + 0.5f * (float)levelsizez;
		int hmlevellengthx = (int)(heightmapx * verhoudingx);
		int hmlevellengthz = (int)(heightmapz * verhoudingz);
		int hmlevelbeginx  = (int)(0.5 * heightmapx - 0.5 * hmlevellengthx);
		int hmleveleindx   = (int)(0.5 * heightmapx + 0.5 * hmlevellengthx);
		int hmlevelbeginz  = (int)(0.5 * heightmapx - 0.5 * hmlevellengthz);
		int hmleveleindz   = (int)(0.5 * heightmapx + 0.5 * hmlevellengthz);

		for (int x = 0; x < heightmapx; x++) {
			for(int z = 0; z < heightmapz; z++){
				heights[x,z] = beginhoogte;
			}
		}
		
		terraindata.SetHeights(0,0,heights);
	

		// heuvels genereren in het level
		for (int i = 0; i < aantalHeuvels; i++) {
			int minradius = (int)(terrainsizex/150);
			int maxradius = (int)(terrainsizex/110);
			int heuvelradius = (int) (Random.Range(minradius,maxradius));
			int xbegin = (int) (Random.Range (hmlevelbeginx+maxradius,hmleveleindx-maxradius));
			int zbegin = (int) (Random.Range (hmlevelbeginz+maxradius,hmleveleindz-maxradius));
			float top = 0.03f*(terrainsizex/2000);
			float helling = (float)(top*(0.002f/0.03f));
			for(int r = 1; r < heuvelradius; r++){
				for(int d = 0; d < 360; d++){
					int x = xbegin + (int)(r*Mathf.Cos(d*Mathf.PI/180));
					int z = zbegin + (int)(r*Mathf.Sin(d*Mathf.PI/180));
					float hoogte = Mathf.Max ((float)(top-(Mathf.Pow((r*helling),2))),(float)(beginhoogte*1.1));
					if(heights[Mathf.Max(x,0),Mathf.Max(z,0)]<hoogte){
						heights[Mathf.Max(x,0),Mathf.Max(z,0)] = hoogte;
					}
				}
			}
		}

		// vijvers genereren level
		for (int i = 0; i < aantalVijvers; i++) {
			int minradius = (int)(terrainsizex/150);
			int maxradius = (int)(terrainsizex/110);
			int heuvelradius = (int) (Random.Range(minradius,maxradius));
			int xbegin = (int) (Random.Range (hmlevelbeginx+maxradius,hmleveleindx-maxradius));
			int zbegin = (int) (Random.Range (hmlevelbeginz+maxradius,hmleveleindz-maxradius));
			for(int r = 1; r < heuvelradius; r++){
				for(int d = 0; d < 360; d++){
					int x = xbegin + (int)(r*Mathf.Cos(d*Mathf.PI/180));
					int z = zbegin + (int)(r*Mathf.Sin(d*Mathf.PI/180));
					heights[x,z] = 0;
				}
			}
		}

		// bergen genereren omgeving
		for(int i = 0; i < aantalBergen; i++){
			int xbegin = (int)Random.Range(0,beginx);
			int zbegin = (int)Random.Range(0,terrainsizez);
			print (xbegin + " " + zbegin);
			for(int r = 0; r < 200; r++){
				for(int a = 0; a < 360; a++){
					int x = Mathf.Min ((int)(xbegin + r*Mathf.Cos(a*Mathf.PI/180)),hmlevellengthx-1);
					int xx = Mathf.Max(x,0);
					int z = Mathf.Min ((int)(zbegin + r*Mathf.Sin(a*Mathf.PI/180)),hmlevellengthz-1);
					int zz = Mathf.Max(z,0);
					heights[xx, zz] = 0.5f-0.025f*r+0*(Random.Range(-0.0005f,0.0005f));
				}
			}
		}
		GameObject hekPrefab = Resources.Load ("Hekje") as GameObject;
		float lengteHek = hekPrefab.GetComponent<Renderer>().bounds.size.x*0.95f;
		int aantalhekjesx = (int)(levelsizex / lengteHek);
		int aantalhekjesz = (int)(levelsizez / lengteHek);
		for (int i = 1; i < aantalhekjesx+2; i++) {
			GameObject hek = Instantiate(hekPrefab);
			hek.transform.position = new Vector3 (beginx + i * lengteHek, terrain.transform.position.y, beginz);;
		}
		for (int i = 1; i < aantalhekjesx+2; i++) {
			GameObject hek2 = Instantiate(hekPrefab);
			hek2.transform.position = new Vector3 (beginx + i * lengteHek, terrain.transform.position.y, eindz);
		}
		for (int i = 0; i < aantalhekjesz+1; i++) {
			GameObject hek = Instantiate(hekPrefab);
			hek.transform.position = new Vector3 (beginx, terrain.transform.position.y, beginz+ i*lengteHek);
			hek.transform.eulerAngles = new Vector3(0,90,0);
		}
		for (int i = 0; i < aantalhekjesz+1; i++) {
			GameObject hek2 = Instantiate(hekPrefab);
			hek2.transform.position = new Vector3 (eindx, terrain.transform.position.y, beginz + i*lengteHek);
			hek2.transform.eulerAngles = new Vector3(0,90,0);
		}
		terraindata.SetHeights (0, 0, heights);
		for (int i = 0; i < 3; i++) {
			Smooth();
		}
	}
	private void Smooth()
	{
		float[,] height = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapWidth,
		                                                 terrain.terrainData.heightmapHeight);
		float k = 0.5f;
		for (int x = 1; x < terrain.terrainData.heightmapWidth; x++)
			for (int z = 0; z < terrain.terrainData.heightmapHeight; z++)
				height[x, z] = height[x - 1, z] * (1 - k) +	height[x, z] * k;
		
		for (int x = terrain.terrainData.heightmapWidth - 2; x < -1; x--)
			for (int z = 0; z < terrain.terrainData.heightmapHeight; z++)
				height[x, z] = height[x + 1, z] * (1 - k) +height[x, z] * k;
		
		for (int x = 0; x < terrain.terrainData.heightmapWidth; x++)
			for (int z = 1; z < terrain.terrainData.heightmapHeight; z++)
				height[x, z] = height[x, z - 1] * (1 - k) +	height[x, z] * k;
		
		for (int x = 0; x < terrain.terrainData.heightmapWidth; x++)
			for (int z = terrain.terrainData.heightmapHeight; z < -1; z--)
				height[x, z] = height[x, z + 1] * (1 - k) + height[x, z] * k;
		
		terrain.terrainData.SetHeights(0, 0, height);
	}
	// Use this for initialization
	void Start () {
		generateTerrain ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
