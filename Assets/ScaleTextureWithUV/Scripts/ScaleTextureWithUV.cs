using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTextureWithUV : MonoBehaviour {


    readonly int STANDARD_TEXTURE_SIZE = 100; //世界1个单位，对应100像素的贴图； 
	// Use this for initialization
	void Start () {
        //this.transform.position = Vector3.zero;
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Material[] mats = this.GetComponent<MeshRenderer>().sharedMaterials;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = mesh.uv;

        for (int k = 0; k < mesh.subMeshCount; k++)
        {
            string name = mats[k].mainTexture.name;
            if (name != "lm_ly1") continue;
            int[] triangles = mesh.GetTriangles(k);
            //Debug.Log(name);
            //Debug.Log(vertices[triangles[0]] + "," + uvs[triangles[0]]);
            //Debug.Log(vertices[triangles[1]] + "," + uvs[triangles[1]]);

            int vertexIndexA = triangles[0];
            int vertexIndexB = triangles[1];
            CreatePrimitive(name + "-A", vertices[vertexIndexA]);
            CreatePrimitive(name + "-B", vertices[vertexIndexB]);

            float v_distacne = GetDistanceWithVertex(vertices[vertexIndexA], vertices[vertexIndexB]); //两个顶点的距离
            float uv_distacne = GetDistanceWithUV(uvs[vertexIndexA], uvs[vertexIndexB]);//两个顶点对应uv的距离
            int standard_texture_pixel = Mathf.Max(mats[k].mainTexture.width, mats[k].mainTexture.height);//1个uv单位对应的贴图像素

            float current_size = uv_distacne * standard_texture_pixel; //两个顶点间的实际贴图像素尺寸；
            float target_size = v_distacne * STANDARD_TEXTURE_SIZE;//两个顶点间的理论贴图像素尺寸；

            Debug.Log(target_size / current_size);
        }
    }

    void CreatePrimitive(string name,Vector3 position)
    {

        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.name = name;
        //TransformPoint转变会受物体的位置和缩放影响转换，而TransformVector仅受物体的缩放影响转换。
        go.transform.position = this.transform.TransformPoint(position);
        go.transform.localScale = new Vector3(1, 1, 1);
        go.GetComponent<MeshRenderer>().material.color = Color.red;

    }

    float GetDistanceWithVertex(Vector3 a, Vector3 b)
    {
        //Debug.Log(Vector3.Distance(a, b) + "---" + Vector3.Distance(this.transform.TransformVector(a), this.transform.TransformVector(b)));
        return Vector3.Distance(a, b);
    }

    float GetDistanceWithUV(Vector2 a, Vector2 b)
    {
        return Vector2.Distance(a, b);
    }
}
