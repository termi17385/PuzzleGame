using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace PuzzleGame.Collectables
{
    public class Collectables : MonoBehaviour
    {
        #region Variables
        public int collectedCollectables = 0;
        public Text collectedAmount;
        //public Tilemap collectableTiles;
        #endregion

        private void FixedUpdate()
        {
            collectedAmount.text = collectedCollectables.ToString("Collected Parts: " + collectedCollectables);     // writes to the Ui text and adds the interger value 
        }
        //private void OnCollisionEnter2D(Collision2D pickup)
        //{
        //    if (pickup.gameObject.CompareTag("Collectables"))                                                       // If a pickup with the tag "Collectables" is hit
        //    {
        //        collectedCollectables++;                                                                            // Adds to the integer value
        //        Vector3 hitpos = Vector3.zero;                                                                      // zeros out Vector3 points
        //        foreach (ContactPoint2D hit in pickup.contacts)                                                     // cycles through contact points 
        //        {
        //            hitpos.x = hit.point.x - 0.1f * hit.normal.x;                                                   // Detects the X point with the objects surface
        //            hitpos.y = hit.point.y - 0.1f * hit.normal.y;                                                   // Detects the y point with the objects surface
        //            collectableTiles.SetTile(collectableTiles.WorldToCell(hitpos), null);                           // Sets the tile to null in order to destroy the specific tile
        //        }
        //    }
        //}
        private void OnTriggerEnter2D(Collider2D pickup)
        {
            if (pickup.gameObject.CompareTag("Collectables"))
            {
                collectedCollectables++;
                Destroy(pickup.gameObject);
            }
        }
    }
}

