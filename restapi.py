from flask import Flask, request
from flask_restful import Api, Resource
import pandas as pd

app = Flask(__name__)
api = Api(app)

class Character(Resource):
    def get(self):
        data = pd.read_csv('karakter.csv')
        data = data.to_dict('records')
        return {'data' : data}, 200

    def post(self):
        isim = request.args['isim']
        element = request.args['element']
        silah = request.args['silah']

        data = pd.read_csv('karakter.csv')

        new_data = pd.DataFrame({
            'isim'      : [isim],
            'element'       : [element],
            'silah'      : [silah]
        })

        data = data.append(new_data, ignore_index = True)
        data.to_csv('karakter.csv', index=False)
        return {'data' : new_data.to_dict('records')}, 200

    def delete(self):
        name = request.args['isim']
        data = pd.read_csv('karakter.csv')
        data = data[data['isim'] != isim]

        data.to_csv('karakter.csv', index=False)
        return {'message' : 'Record deleted successfully.'}, 200

class Element(Resource):
    def get(self):
        data = pd.read_csv('karakter.csv',usecols=[2])
        data = data.to_dict('records')
        
        return {'data' : data}, 200

class Name(Resource):
    def get(self,name):
        data = pd.read_csv('karakter.csv')
        data = data.to_dict('records')
        for entry in data:
            if entry['isim'] == isim :
                return {'data' : entry}, 200
        return {'message' : 'No entry found with this name !'}, 404


# Add URL endpoints
api.add_resource(Character, '/karakter')
api.add_resource(Element, '/element')
api.add_resource(Name, '/<string:isim>')


if __name__ == '__main__':
    app.run(host="0.0.0.0", port=5000)
    app.run()