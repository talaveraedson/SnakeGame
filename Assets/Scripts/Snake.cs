using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Snake

public class Snake : MonoBehaviour

{

    #region Properties

    enum Movements
    {

        left,//Movimiento de la Serpiente a la Izquierda

        right,//Movimiento de la Serpiente a la Derecha

        up,//Movimiento de la Serpiente a Arriba

        down//Movimiento de la Serpiente a Abajo 

    }


    Movements _movement;


    public float _frameRate = 0.2f;


    public float _nextStep = 0.16f;


    Vector3 _lastPos;

    public BoxCollider2D gridArea;

    public List<Transform> _body = new List<Transform>();

    public Transform _bodyPrefab;

    public Transform _tailPrefab;



    #endregion

    #region Start

    void Start()

    {
        RandomizePosition();

        transform.localRotation = Quaternion.Euler(0, 0, -90);
        _body[0].rotation = Quaternion.Euler(0, 0, 90);
        InvokeRepeating("Move", _frameRate, _frameRate);

    }

    #endregion

    #region Methods


    void Move()
    {


        _lastPos = transform.position;


        Vector3 nextPos = Vector3.zero;


        if (_movement == Movements.up)
        {


            nextPos = Vector3.up;

        }
        else if (_movement == Movements.down)
        {

            //Vector3.movimiento en este caso hacia abjao

            nextPos = Vector3.down;

        }
        else if (_movement == Movements.left)
        {

            //Vector3.movimiento en este caso hacia la izquierda

            nextPos = Vector3.left;

        }
        else if (_movement == Movements.right)
        {

            //Vector3.movimiento en este caso hacia la derecha

            nextPos = Vector3.right;

        } 

        nextPos *= _nextStep;
        transform.position += nextPos;
        MoveBody();

    }

    public void MoveBody()
    {
        for (int i = 0; i < _body.Count; i++)
        {
            Vector3 temp = _body[i].position;
            _body[i].position = _lastPos;
            _lastPos = temp;
        }
    }




    #endregion



    #region Update

    // Update is called once per frame

    /*private void Start()
    {
        RandomizePosition();
    }*/

    public void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        // Pick a random position inside the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Round the values to ensure it aligns with the grid
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y),0.0f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Borders"))
        {
            print("Game Over");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else if (collider.CompareTag("Apple"))
        {
            _body.Add(Instantiate(_bodyPrefab, _body[_body.Count-1].position,Quaternion.identity).transform);
            print("You ate the Apple");
        }
        
    }
    
    void Update()

    {

        //Compararemos las entradas del player para compararlas con los movimeintos que estan

        //declarados en la variable Movements left, right, up y down.

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            //Si cumple asignamos ese valor a la variable derivada de Movements : movement hacia arriba

            _movement = Movements.up;

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            //Si cumple asignamos ese valor a la variable derivada de Movements : movement hacia abajo

            _movement = Movements.down;

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            //Si cumple asignamos ese valor a la variable derivada de Movements : movement hacia la izquierda

            _movement = Movements.left;

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            //Si cumple asignamos ese valor a la variable derivada de Movements : movement hacia la derecha

            _movement = Movements.right;

        }

    }

    

    #endregion
}

#endregion
