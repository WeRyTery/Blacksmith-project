using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class E_EventBus
{
    public static Action NewOrder;

    public static Action LoadSavedData;

    public static Action ResetUXafterSmithingMechanic;
    public static Action EnableSmithingMechanicUI;

    public static Action NewDay;
}
