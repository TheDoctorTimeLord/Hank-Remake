using Core;
using Core.Utils;
using Gameplay.Level;
using Model;
using UnityEngine;

namespace Mechanics
{
    [RequireComponent(typeof(Camera))]
    public class CameraCustomBehavior : MonoBehaviour
    {
        [SerializeField] private GameObject targetWatching;
        
        private LevelCapability _levelCapability;
        private Camera _camera;
        private Vector2 _leftBottomClamping;
        private Vector2 _rightTopClamping;
        
        private void Start()
        {
            _camera = GetComponent<Camera>();
            var halfHeight = _camera.orthographicSize;
            var halfWidth = _camera.aspect * halfHeight;

            _levelCapability = Simulation.GetCapability<LevelCapability>();
            
            var fieldSides = _levelCapability.SidesInWorldCoordinate;

            float xLeft; //TODO выглядит уродливо
            float xRight;
            float yTop;
            float yBottom;

            if (halfWidth * 2 < fieldSides.x)
            {
                xLeft = halfWidth;
                xRight = fieldSides.x - halfWidth;
            }
            else
            {
                xLeft = fieldSides.x / 2;
                xRight = fieldSides.x / 2;
            }

            if (halfHeight * 2 < fieldSides.y)
            {
                yBottom = halfHeight;
                yTop = fieldSides.y - halfHeight;
            }
            else
            {
                yBottom = fieldSides.y / 2;
                yTop = fieldSides.y / 2;
            }
            
            _leftBottomClamping = new Vector2(xLeft, yBottom);
            _rightTopClamping = new Vector2(xRight, yTop);
        }

        private void Update()
        {
            var newTargetPosition = (Vector2)targetWatching.transform.position;
            var newCameraPosition = newTargetPosition.Clamp(_leftBottomClamping, _rightTopClamping);
            transform.position = new Vector3(newCameraPosition.x, newCameraPosition.y, transform.position.z);
        }
    }
}