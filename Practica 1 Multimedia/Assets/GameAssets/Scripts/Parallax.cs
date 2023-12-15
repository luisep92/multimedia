using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform[] tiles;
    private float bottomPoint;
    private float height;

    private void Start()
    {
        tiles = GetTiles();
        height = tiles[0].GetComponent<SpriteRenderer>().size.y;
        SetInitialPositions(height);

        bottomPoint = tiles[0].position.y - height;
    }
     
    void Update()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            float quantity = -speed * Time.deltaTime;
            tiles[i].Translate(0, quantity, 0);
            if (tiles[i].position.y <= bottomPoint)
                MoveToTop(tiles[i], i, quantity);
        }

    }

    private void SetInitialPositions(float height)
    {
        for (int i = 1; i < tiles.Length; i++)
        {
            Vector3 tilePos = tiles[i].position;
            tiles[i].transform.position = new Vector3(tilePos.x, tilePos.y + (height * i), tilePos.z);
        }
    }

    private Transform[] GetTiles()
    {
        int size = transform.childCount;
        Transform[] ret = new Transform[size];
        for(int i = 0; i < size; i++)
        {
            ret[i] = transform.GetChild(i);
        }
        return ret;
    }

    private void MoveToTop(Transform tile, int index, float offset)
    {
        tile.position = new Vector3(tile.position.x, GetTopPosition(index) + height + offset, tile.position.z);
    }

    private float GetTopPosition(int index)
    {
        if(index == 0)
            return tiles[^1].position.y;
        return tiles[index - 1].position.y;
    }
}
