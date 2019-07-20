# FurnaceSerializer

A modular serializer built with networking in mind.


## Features
- Serialize & deserialize **objects**, without worrying about the bytes behind the scenes
- 13 built-in field types + arrays for any supported type
- Easily expand the serializer with your own field types
- Built with networking in mind: great for serializing packets and deserializing them when received from the network 
- The serializer knows how to differienciate between object types and is therefore able to yield an object from a byte array

## Usage Sample
Here's a example where we have an inventory of items, represented as strings, which we want to save into a persistent format(byte[]).
```csharp    
FurnaceSerializer _serializer;

void Setup() // Constructor or init method. Run only once
{
      _serializer = new FurnaceSerializer();

      _serializer.RegisterType(typeof(string[])); // The string type is built-in, but arrays for any type need to be explicitely registered
      _serializer.RegisterType(typeof(ItemStack)); // IMPORTANT: Registered types and serializers must be registered in the SAME order if
      //                                              using two different instances of the FurnaceSerializer. (ie: client-server scenario)
}

byte[] SaveInventory(ItemStack inv)
{
      // We can serialize ItemStack's because it was registered earlier.
      byte[] serializedObject = serializer.Serialize(inv);
		
      return serializedObject;
}
	
ItemStack LoadInventory(byte[] data)
{
      // The serializer automatically knows 'data' corresponds to an ItemStack object
      object deserializedObject = serializer.Deserialize(data);

      return (ItemStack) object;
}
```
..and here is the code for ItemStack:
```csharp
struct ItemStack
{
      // All fields you want to serialize need to be marked with the attribute "FurnaceSerializable"
      [FurnaceSerializable] public string[] Items;

      ItemStack(params string[] items)
      {
            Items = items;
      }
}
```
The serializer isn't limited to structs, but using classes isn't recommended because they absolutely *must* have a default constructor.

## Custom Field Types
The FurnaceSerializer includes 13 system types + arrays, but isn't limited to these. You can define your own field types by implementing the ISerializer interface:
```csharp
class Vector2Serializer : ISerializer
{
      public Type Type => typeof(Vector2);

      public int SizeOf(object value) => sizeof(float) * 2;

      public bool Write(object value, byte[] buffer, ref int position)
      {
            Vector2 vec = (Vector2)value;

            // Use static SerializerUtil for common serializer operations
            return SerializerUtil.WriteFloat(vec.X, buffer, ref position)
                  && SerializerUtil.WriteFloat(vec.Y, buffer, ref position); 
      } 

      public object Read(byte[] buffer, ref int position, bool peek = false)  
      {  
            float x = SerializerUtil.ReadFloat(buffer, ref position, peek);  
            float y = SerializerUtil.ReadFloat(buffer, ref position, peek);  
            return new Vector2(x, y);  
      }
}
```
...you then need to register this serializer via `_serializer.RegisterSerializer(new Vector2Serializer());`
Now, the serializer can handle Vector2's and can even do: `_serializer.RegisterType(typeof(Vector2[]));`
