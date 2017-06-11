//
// Gargore AIRCRAFT TOOLKIT (bundle edition, version 0.83)
//

//
// IMPORTANT NOTICE - this file should not be directly edited, if you really need to
//                    modify it, it's better if you create a children class or subclass.
//                    If you need more info, please check the manual.

using UnityEngine;
using System.Collections;

public interface GWindInterface {
	Vector3 windAtImplementation(Vector3 position);
}

