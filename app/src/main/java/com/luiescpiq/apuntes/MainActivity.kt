package com.luiescpiq.apuntes

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import com.google.firebase.firestore.CollectionReference
import com.google.firebase.firestore.DocumentReference
import com.google.firebase.firestore.FirebaseFirestore
import com.luiescpiq.apuntes.databinding.ActivityMainBinding

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    private lateinit var db: FirebaseFirestore
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)
        // Obtenemos la instancia a la BD
        db = FirebaseFirestore.getInstance()
        // Obtenemos la colección con la que deseamos trabajar
        val profesCollection: CollectionReference = db.collection("Profesores")
        // Obtener un documento concreto.
        val docRef: DocumentReference = profesCollection.document("1")
        //obtenerElementoSinEscuchaActiva(docRef)

        //Obtener todos los elementos
       //obtenerTodosLosElementosSinEscucha(profesCollection)

        //Obtener usando where
        obtenerUtilizandoWhereSinyConEscucha(profesCollection)

        // Añadir
        binding.btnAdd.setOnClickListener {
            // Se crea la estructura del documento.
            val profe = hashMapOf(
                "nombre" to "Pedro",
                "apellido" to "Prieto",
                "modulo" to "DI"
            )
            // Se añade el documento sin indicar ID, dejando que Firebase genere el ID
            // al añadir el documento. Para esta acción se recomienda add().
            profesCollection.document().set(profe)
                // Respuesta si ha sido correcto.
                .addOnSuccessListener {
                    Log.d("DOC_SET", "Documento añadido!")
                }
                // Respuesta si se produce un fallo.
                .addOnFailureListener { e ->
                    Log.w("DOC_SET", "Error en la escritura", e)
                }
            startActivity(Intent(this, RecViewActivity::class.java))
        }

    }
    fun obtenerElementoSinEscuchaActiva(docRef: DocumentReference) {
        docRef.get().apply {
            // Obtiene información, se lanza sin llegar a terminar la conexión.
            addOnSuccessListener {
                Log.d("addOnSuccessListener", "Cached document data: ${it.data}")
                val texto = it["modulo"].toString() + " - " +
                        it["nombre"].toString() + " " +
                        it["apellido"].toString()
                binding.tvShowData.text = texto
            }
            // Fallo de lectura.
            addOnFailureListener { exception ->
                Log.d("addOnFailureListener", "Fallo de lectura ", exception)
            }
        }
    }
    private fun obtenerTodosLosElementosSinEscucha(profesCollection: CollectionReference) {
        // Obtiene todos los documentos de una colección (sin escucha).
        profesCollection.get().apply {
            addOnSuccessListener {
                for (document in it) {
                    Log.d("DOC", "${document.id} => ${document.data}")
                    binding.tvShowData.append(
                        document!!["modulo"].toString() + " - " +
                                document["nombre"].toString() + " " +
                                document["apellido"].toString() + "\n"
                    )
                }
            }
            addOnFailureListener { exception ->
                Log.d("DOC", "Error durante la recogida de documentos: ", exception)
            }
        }
    }

    private fun obtenerUtilizandoWhereSinyConEscucha(profesCollection:
                                                     CollectionReference) {
        // Obtiene todos los documentos de una colección (sin escucha).
        profesCollection.whereEqualTo("modulo", "PMDM").get().apply {
            addOnSuccessListener {
                binding.tvShowData.text = "Sin escucha\n"
                for (document in it) {
                    Log.d("DOC", "${document.id} => ${document.data}")
                    binding.tvShowData.append(
                        document!!["modulo"].toString() + " - " +
                                document["nombre"].toString() + " " +
                                document["apellido"].toString() + "\n"
                    )
                }
            }
            addOnFailureListener { exception ->
                Log.d("DOC", "Error durante la recogida de documentos: ", exception)
            }
        }
        // Obtiene todos los documentos de una colección (con escucha).
        profesCollection.whereEqualTo("modulo", "Programación")
            .addSnapshotListener { querySnapshot, firebaseFirestoreException ->
                if (firebaseFirestoreException != null) {
                    Log.w("addSnapshotListener", "Escucha fallida!.",
                        firebaseFirestoreException)
                    return@addSnapshotListener
                }
                binding.tvShowData2.text = "Con escucha\n"
                for (document in querySnapshot!!) {
                    Log.d("DOC", "${document.id} => ${document.data}")
                    binding.tvShowData2.append(
                        document!!["modulo"].toString() + " - " +
                            document["nombre"].toString() + " " +
                            document["apellido"].toString() + "\n"
                    )
                }
            }
        }
    }
