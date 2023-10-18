package com.luisep.tests

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import kotlinx.serialization.*
import kotlinx.serialization.json.*

@Serializable
data class Person (val name: String, val age: Int, val mail: String)
class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        // Serialize
        val p = Person("Constan", 19, "konstan@gmail.com")
        val str = Json.encodeToString(p)

        //Deserialize
        val str2 = "{\"name\":\"Pepe\", \"age\":25,\"mail\":\"viyuela@gmail.com\"}"
        val p2 = Json.decodeFromString<Person>(str2)
    }
}

