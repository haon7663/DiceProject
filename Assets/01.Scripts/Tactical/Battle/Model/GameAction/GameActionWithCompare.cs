using System;
using System.Collections.Generic;

[Serializable]
public class GameActionWithCompare
{
    public CompareInfo compareInfo;
    public GameAction action;

    public GameAction Value => action;
}