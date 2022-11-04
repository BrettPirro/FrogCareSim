using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frog.ResourceGen;

namespace Frog.Gen 
{
    public class WaterFilterPositionalGen : MonoBehaviour
    {
        [SerializeField] Vector2 RowDistribution;
        [SerializeField] float DistDiff = 0.6f;
        [SerializeField] Transform bodySprite;


        List<Vector2> DistributedPoints = new List<Vector2>();

        Vector2 InitalCornorPos;




        private void Awake()
        {
            InitalCornorPos = transform.position;

            PositionalDataGen(RowDistribution);
        }

        private void Start()
        {
            WaterCollectionManagament.current.FilterPositionUpdate += RelocatebasedonColor;
        }




        private void PositionalDataGen(Vector2 Distribution)
        {


            DistributedPoints.Clear();
            DistributedPoints.Add(InitalCornorPos);




            for (int xValueCount = 0; xValueCount <= RowDistribution.x; xValueCount++)
            {
                float XGen = 0;
                Vector2 Ref = DistributedPoints[xValueCount];
                XGen = Ref.x + DistDiff;
                DistributedPoints.Add(new Vector2(XGen, InitalCornorPos.y));
            }
            for (int yValueCount = 0; yValueCount <= RowDistribution.y; yValueCount++)
            {
                float YGen = 0;
                Vector2 Ref = DistributedPoints[yValueCount];
                YGen = Ref.y + DistDiff;
                DistributedPoints.Add(new Vector2(InitalCornorPos.x, YGen));

            }

        }



        public void RelocatebasedonColor(object refPositionalData, Color CurrentColor)
        {
            //convert the vector 2 positional data to index data to compare to the indexs of waterblock.index

            int randomValGen = Random.Range(0, DistributedPoints.Count);

            randomValGen = GenPoint(randomValGen);

            foreach (var Index in (Dictionary<Vector2, Color>)refPositionalData)
            {
               


                if ((Index.Value == CurrentColor && Index.Key == PositionalConversion(DistributedPoints[randomValGen])))
                {
                    randomValGen = GenPoint(randomValGen);
                }
                else
                {
                    transform.position = DistributedPoints[randomValGen];
                    bodySprite.GetComponent<SpriteRenderer>().color = CurrentColor;
                    break;
                }


            }









        }

        private int GenPoint(int randomValGen)
        {
            while (DistributedPoints[randomValGen].x == transform.position.x)
            {
                randomValGen = Random.Range(0, DistributedPoints.Count);
            }

            return randomValGen;
        }

        private Vector2 PositionalConversion(Vector2 Pos)
        {
            int xVal = 0;
            int yVal = 0;



            for (int val = 0; val >= DistributedPoints.Count; val++)
            {
                if (DistributedPoints[val].x == 0) { continue; }
                float ConversionX = (DistDiff * val) + InitalCornorPos.x;

                if (Pos.x == ConversionX) { xVal = val; break; }


            }

            for (int val = 0; val >= DistributedPoints.Count; val++)
            {
                if (DistributedPoints[val].y == 0) { continue; }
                float ConversionY = (DistDiff * val) + InitalCornorPos.y;

                if (Pos.y == ConversionY) { yVal = val; break; }


            }


            return new Vector2(xVal, yVal);


        }










    }
}
