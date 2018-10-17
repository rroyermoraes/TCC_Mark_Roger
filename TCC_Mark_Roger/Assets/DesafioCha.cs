using UnityEngine;

namespace Desafio8 {

    public class DesafioCha : MonoBehaviour {


        private Tea tea;
        private Liquid liquid;
        private Fuel fuel;


        public void CheckForComplete()
        {
            if (tea != null && liquid != null && fuel != null) {
                Debug.Log("Completo");
            }
            else
            {
                Debug.Log("Está faltando alguma coisa para eu fazer o chá, eu preciso de alguma erva pra dar gosto, água e alguma coisa pra esquentar a máquina.");
            }

        }
        public void SetTea(Tea t)
        {
            tea = t;
        }
        public void SetLiquid(Liquid l)
        {
            liquid = l;
        }
        public void SetFuel(Fuel f)
        {
            fuel = f;
        }

}
}
