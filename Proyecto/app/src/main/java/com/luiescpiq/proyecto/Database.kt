package com.luiescpiq.proyecto

import android.util.Log
import com.google.firebase.firestore.CollectionReference
import com.google.firebase.firestore.FirebaseFirestore

// Ya que se usa la conexión en varias actividades, usamos un singleton con la conexión
// para que solo haya una.
class Database {
    private val connection = FirebaseFirestore.getInstance()

    fun getConnection(): FirebaseFirestore {
        return connection
    }
    companion object {
        val instance = Database()
    }

    // Insertar juego, también la usamos para actualizar en caso de que exista la id.
    fun insertGame(name: String, genre: String, description: String, image: String, score: Float, id: String){
        val game = hashMapOf(
            "name" to name,
            "category" to genre,
            "description" to description,
            "image" to image,
            "score" to score
        )
        // Añadir juego nuevo.
        if (id.isNullOrBlank())
            connection.collection("Games").document().set(game)
                .addOnSuccessListener {
                    Log.d("DOC_SET", "Added game succesfully.")
                }
                .addOnFailureListener { e ->
                    Log.w("DOC_SET", "Error inserting game.", e)
                }
        // Editar juego existente
        else
            connection.collection("Games").document(id).set(game)
                .addOnSuccessListener {
                    Log.d("DOC_SET", "Updated game succesfully.")
                }
                .addOnFailureListener { e ->
                    Log.w("DOC_SET", "Error updating game.", e)
                }
    }

    // Llenamos el recycler view con todos los juegos
    fun addItemsToList(list: MutableList<MyGame>, adapter: GameAdapter) {
        val gamesCollection = connection.collection("Games")
        gamesCollection.addSnapshotListener { querySnapshot, firestoreException ->
            if (firestoreException != null) {
                Log.w("addSnapshotListener", "Escucha fallida!.", firestoreException)
                return@addSnapshotListener
            }
            list.clear()
            for (document in querySnapshot!!) {
                val name = document!!["name"].toString()
                val genre = document["category"].toString()
                val description = document["description"].toString()
                val image = document["image"].toString()
                val score = document["score"]
                val scoreFloat = if (score is Number) score.toFloat() else 0.0f
                val id = document.id
                list.add(MyGame(name, genre, description, image, scoreFloat, id))
            }
            adapter.notifyDataSetChanged()
        }
    }

    // Llenamos el recycler view con los que tienen 5 estrellas
    fun addBestItemsToList(list: MutableList<MyGame>, adapter: GameAdapter) {
        val gamesCollection = connection.collection("Games").whereEqualTo("score", 5.0f)
        gamesCollection.addSnapshotListener { querySnapshot, firestoreException ->
            if (firestoreException != null) {
                Log.w("addSnapshotListener", "Escucha fallida!.", firestoreException)
                return@addSnapshotListener
            }
            list.clear()
            for (document in querySnapshot!!) {
                val name = document!!["name"].toString()
                val genre = document["category"].toString()
                val description = document["description"].toString()
                val image = document["image"].toString()
                val score = document["score"]
                val scoreFloat = if (score is Number) score.toFloat() else 0.0f
                val id = document.id
                list.add(MyGame(name, genre, description, image, scoreFloat, id))
            }
            adapter.notifyDataSetChanged()
        }
    }

    // Borrar juego.
    fun deleteItem(id: String) {
        val game = connection.collection("Games").document(id)
        game
            .delete()
            .addOnSuccessListener {
                Log.d("DOC_UPD", "Documento eliminado correctamente")
            }
            .addOnFailureListener { e ->
                Log.w("DOC_UPD", "Error al eliminar el documento", e)
            }
    }
}