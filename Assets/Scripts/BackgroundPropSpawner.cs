using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制背景汽车开动
/// </summary>
public class BackgroundPropSpawner : MonoBehaviour {

    [SerializeField] Rigidbody2D backgroundProp;      // The prop to be instantiated.
    [SerializeField] float leftSpawnPosX;             // The x coordinate of position if it's instantiated on the left.
    [SerializeField] float rightSpawnPosX;            // The x coordinate of position if it's instantiated on the right.
    [SerializeField] float minSpawnPosY;              // The lowest possible y coordinate of position.
    [SerializeField] float maxSpawnPosY;              // The highest possible y coordinate of position.
    [SerializeField] float minTimeBetweenSpawns;      // The shortest possible time between spawns.
    [SerializeField] float maxTimeBetweenSpawns;      // The longest possible time between spawns.
    [SerializeField] float minSpeed;                  // The lowest possible speed of the prop.
    [SerializeField] float maxSpeed;                  // The highest possible speeed of the prop.

    // Use this for initialization
    void Start () {
        // Set the random seed so it's not the same each game.
        // 设置随机的种子，所以它不是相同的每一个游戏。
        Random.InitState(System.DateTime.Today.Millisecond);

        // Start the Spawn coroutine.
        // 开始产生协同程序。
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn() {
        // Create a random wait time before the prop is instantiated.
        //创建一个随机的等待时间，然后再实例化。
        float waitTime = Random.Range(minTimeBetweenSpawns,maxTimeBetweenSpawns);

        // Wait for the designated period.
        // 等待指定的时间。
        yield return new WaitForSeconds(waitTime);

        // Randomly decide whether the prop should face left or right.
        // 随机判断道具是否应该向左或向右。
        bool facingLeft = Random.Range(0,2) == 0;

        // If the prop is facing left, it should start on the right hand side, otherwise it should start on the left.
        // 如果道具向左，它应该从右边开始，否则它应该从左边开始。
        float posX = facingLeft ? rightSpawnPosX : leftSpawnPosX;

        // Create a random y coordinate for the prop.
        // 为这个道具创建一个随机的y坐标。
        float posY = Random.Range(minSpawnPosY,maxSpawnPosY);

        // Set the position the prop should spawn at.
        // 设置该支柱应该衍生的位置。
        Vector3 spawnPos = new Vector3(posX,posY,transform.position.z);

        // Instantiate the prop at the desired position.
        // 实例化支持的位置。
        Rigidbody2D propInstance = Instantiate(backgroundProp,spawnPos,Quaternion.identity) as Rigidbody2D;

        // The sprites for the props all face left.  Therefore, if the prop should be facing right...
        // 所有道具的精灵都在左边。因此，如果支柱应该面向右……
        if (!facingLeft)
        {
            // ... flip the scale in the x axis.
            // …在x轴上翻转刻度。
            Vector3 scale = propInstance.transform.localScale;
            scale.x *= -1;
            propInstance.transform.localScale = scale;
        }

        // Create a random speed.
        // 创建一个随机的速度。
        float speed = Random.Range(minSpeed,maxSpeed);

        // These speeds would naturally move the prop right, so if it's facing left, multiply the speed by -1.
        // 这些速度会自然地移动道具，所以如果它向左，速度乘以-1。
        speed *= facingLeft ? -1f : 1f;

        // Set the prop's velocity to this speed in the x axis.
        // 将道具的速度设置为x轴的速度。
        propInstance.velocity = new Vector2(speed,0);

        // Restart the coroutine to spawn another prop.
        // 重新启动coroutine以产生另一个道具。
        StartCoroutine(Spawn());

        // While the prop exists...
        // 而道具的存在……
        while (propInstance != null)
        {
            // ... and if it's facing left...
            //…如果它面朝左……
            if (facingLeft)
            {
                // ... and if it's beyond the left spawn position...
                // …如果它超出了左侧的产卵位置…
                if (propInstance.transform.position.x < leftSpawnPosX - 0.5f)
                    // ... destroy the prop.
                    // …破坏支撑。
                    Destroy(propInstance.gameObject);
            }
            else
            {
                // Otherwise, if the prop is facing right and it's beyond the right spawn position...
                // 否则，如果道具正面朝右，并且它超出了正确的产卵位置…
                if (propInstance.transform.position.x > rightSpawnPosX + 0.5f)
                    // ... destroy the prop.
                    Destroy(propInstance.gameObject);
            }

            // Return to this point after the next update.
            // 在下一次更新之后返回到这个点。
            yield return null;
        }
    }

}
