package com.luisep.examen
import android.content.Context
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.google.firebase.firestore.CollectionReference
import com.google.firebase.firestore.FirebaseFirestore
import com.luisep.examen.databinding.ActivityRecViewBinding
import com.luisep.examen.databinding.ItemAppListBinding

class RecViewActivity : AppCompatActivity() {
    private lateinit var binding: ActivityRecViewBinding
    private lateinit var db: FirebaseFirestore
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityRecViewBinding.inflate(layoutInflater)
        setContentView(binding.root)
        db = FirebaseFirestore.getInstance()
        getApps()


    }



    private fun getApps() {
        val apps: MutableList<MyApp> = arrayListOf<MyApp>()
        val appCollection: CollectionReference = db.collection("Apps")
            appCollection.whereEqualTo("tipo", SecondActivity.filter).get()
                .addOnSuccessListener {it ->
                    for (document in it) {
                        Log.d("DOC", "${document.id} => ${document.data}")
                        val app = document!!["aplicacion"].toString()
                        val tema = document["tema"].toString()
                        val horas = document["horas"].toString()
                        val tipo = document["tipo"].toString()
                        apps.add(MyApp(app, tema, horas, tipo))
                    }
                    val myAdapter = DiscoAdapter(apps, this)
                    binding.myRVDiscos.setHasFixedSize(true)
                    binding.myRVDiscos.layoutManager = LinearLayoutManager(this)
                    binding.myRVDiscos.adapter = myAdapter
                }
                .addOnFailureListener { exception ->
                    Log.d("DOC", "Error durante la recogida de documentos: ", exception)
                }
    }
}




data class MyApp (val app: String,
                  val tema: String,
                  val horas: String,
                  val tipo: String)



class DiscoAdapter (discosList: MutableList<MyApp>, context: Context): RecyclerView.Adapter<DiscoAdapter.DiscoViewHolder>() {
    var myDiscos: MutableList<MyApp>
    var myContext: Context
    init {
        myDiscos = discosList
        myContext = context
    }

    // Es el encargado de devolver el ViewHolder ya configurado
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int):
            DiscoViewHolder {
        val view = LayoutInflater
            .from(parent.context)
            .inflate(R.layout.item_app_list, parent, false)
        return DiscoViewHolder(view)
    }
    override fun onBindViewHolder(holder: DiscoViewHolder, position: Int) {
        val item = myDiscos.get(position)
        holder.bind(item, myContext)
    }
    override fun getItemCount(): Int {
        return myDiscos.size
    }
    class DiscoViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        // Se usa View Binding para localizar los elementos en la vista.
        // Evitamos de esa forma la utilización de findViewById
        private val binding = ItemAppListBinding.bind(view)
        fun bind(app: MyApp, context: Context) {
            binding.txtApp.text = app.app
            binding.txtTema.text = app.tema.toString()
            binding.txtHoras.text = app.horas.toString()
            // Definimos el código a ejecutar si se hace click en el item
            itemView.setOnClickListener {
                Toast.makeText(
                    context,
                    app.app,
                    Toast.LENGTH_SHORT
                ).show()
            }
        }
    }
}

