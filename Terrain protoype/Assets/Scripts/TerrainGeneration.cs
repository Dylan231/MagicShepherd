using UnityEngine;
using System.Collections;

public class TerrainGeneration : MonoBehaviour {
	public Terrain terrain;
	private TerrainData terraindata;
	public int aantalHeuvels;
	public int aantalSloten;
	public float[] positionsx;
	public float[] positionsz;

	public void GenerateTerrain(){
		terraindata = terrain.terrainData;
		float[] positionsx = new  float[aantalHeuvels];
		float[] positionsz = new  float[aantalHeuvels];

		int heightmapwidth = terraindata.heightmapWidth-1;
		int heightmapheigth = terraindata.heightmapHeight-1;
		float beginhoogte = 0.003f;
		float[,] heights = new float[heightmapwidth + 1, heightmapheigth + 1];

		for (int x = 0; x < heightmapwidth; x++) {
			for(int z = 0; z < heightmapheigth; z++){
				heights[x,z] = beginhoogte;
			}
		}

		terraindata.SetHeights(0,0,heights);


		for (int i = 0; i < aantalHeuvels; i++) {
			int beginx = (int) (Random.value*heightmapwidth);
			int beginz = (int) (Random.value*heightmapheigth);
			positionsx[i] = beginx;
			positionsz[i] = beginz;
			int heuvelradius = (int) (Random.Range(70,100));
			float top = 0.03f;
			float helling = (float)(top*(0.002f/0.03f));
			for(int r = 1; r < heuvelradius; r++){
				for(int d = 0; d < 360; d++){
					int xx = beginx + (int)(r*Mathf.Cos(d*Mathf.PI/180));
					int x = Mathf.Min(xx,heightmapwidth);
					int zz = beginz + (int)(r*Mathf.Sin(d*Mathf.PI/180));
					int z = Mathf.Min(zz,heightmapheigth);
					float hoogte = Mathf.Max ((float)(top-(Mathf.Pow((r*helling),2))),(float)(beginhoogte*1.1));
					if(heights[Mathf.Max(x,0),Mathf.Max(z,0)]<hoogte){
						heights[Mathf.Max(x,0),Mathf.Max(z,0)] = hoogte;
					}
				}
			}
		}
		// sloten genereren
		for (int i = 0; i < aantalSloten; i++) {
			int beginx = (int) (Random.value*heightmapwidth);
			int beginz = (int) (Random.value*heightmapheigth);
			int slootbreedte = (int)(Random.value*30);
			int slootlengte = (int)(Random.value*300);
			if(Random.value>0.5){
				for(int x = 0; x < slootlengte; x++){
					for(int z = 0; z < slootbreedte; z++){
						int xx = Mathf.Min((beginx + x),heightmapwidth);
						int zz = Mathf.Min((beginz + z),heightmapheigth);
						if(positioncheck(xx, zz, positionsx, positionsz)){
							heights[Mathf.Max(xx,0), Mathf.Max (zz,0)] = 0;
						}
					}
				}
			}
			else{
				for(int x = 0; x < slootbreedte; x++){
					for(int z = 0; z < slootlengte; z++){
						int xx = Mathf.Min((beginx + x),heightmapwidth);
						int zz = Mathf.Min((beginz + z),heightmapheigth);
						if(positioncheck(xx, zz, positionsx, positionsz)){
							heights[Mathf.Max(xx,0), Mathf.Max (zz,0)] = 0;
						}					
					}
				}
			}
		}

		terraindata.SetHeights(0,0,heights);

		for (int i = 0; i < 5; i++) {
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
	public bool positioncheck(float x, float z, float[] xh, float[] zh){
		for(int i = 0; i < aantalHeuvels; i++){
			float disx = x - xh[i];
			float disz = z - zh[i];
			float distance = Mathf.Sqrt(disx*disx + disz*disz);
			if(distance<80){
					return false;
			}
		}
		return true;
	}
	// Use this for initialization
	void Start () {
		GenerateTerrain ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
