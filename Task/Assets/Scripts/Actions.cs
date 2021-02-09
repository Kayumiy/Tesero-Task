using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ActionManager
{
    public class Actions : MonoBehaviour
    {

        public static IEnumerator Moving(Transform ObjectToMove, int currentPosition, List <Vector3> positions, float moveSpeed)
        {
            if (moveSpeed != 0)
            {
                while (ObjectToMove.transform.position != positions[currentPosition])
                {
                    ObjectToMove.transform.position = Vector3.MoveTowards(ObjectToMove.transform.position, positions[currentPosition], moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
            
            

        }

        public static IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
        {
            float elapsedTime = 0;
            Vector3 startingPos = objectToMove.transform.position;
            while (elapsedTime < seconds)
            {
                if (!objectToMove) yield break;

                objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            objectToMove.transform.position = end;
        }

    }

}

