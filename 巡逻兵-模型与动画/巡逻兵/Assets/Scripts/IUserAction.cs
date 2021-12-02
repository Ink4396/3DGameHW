using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void MovePlayer(float speed, float direction);
    void Restart();
}
