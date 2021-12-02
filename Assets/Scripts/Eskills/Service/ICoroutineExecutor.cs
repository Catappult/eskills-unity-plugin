using System.Collections;
using System.ComponentModel;
using UnityEngine;

namespace Eskills.Service
{
    public interface ICoroutineExecutor
    {
        Coroutine StartCoroutine(string methodName);
        Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value);
        Coroutine StartCoroutine(IEnumerator routine);
    }
}